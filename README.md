# A. Logical and Review Code
### 1. How about your opinion..?
```csharp
if (application != null)
{
    if (application.protected != null)
    {
        return application.protected.shieldLastRun;
    }
}
```
**Key:** Cleaner and easier to read code.

```csharp
// Refactored code
return application?.protected?.shieldLastRun;
```
The updated code accomplishes the tasks, as the code. It verifies if the application exists then confirms if application.protected exists. Ultimately retrieves application.protected.shieldLastRun. In case any of the objects, in this process are null it will smoothly return null without triggering a NullReferenceException. This enhances the clarity and brevity of the code making it easier to read.

### 2. How about your opinion..?
```csharp
public ApplicationInfo GetInfo()
{
    var application = new ApplicationInfo
    {
        Path = "C:/apps/",
        Name = "Shield.exe"
    };

    return application;
}
```
**Key:** return more than one value from a class method.

```csharp
// Solution
public (string Path, string Name) GetInfo()
{
    var application = new ApplicationInfo
    {
        Path = "C:/apps/",
        Name = "Shield.exe"
    };
    return (application.Path, application.Name);
}
```
The solution mentioned earlier enables a function to provide two results in a tuple that includes the Path and Name. 

### 3. How about your opinion..?

```csharp
class Laptop
{
    public string Os{ get; set; } // can be modified
    public Laptop(string os)
    {
        Os= os;
    }
}
var laptop = new Laptop("macOs");
Console.WriteLine(Laptop.Os); // Laptop os: macOs
```
**Key:** modifications by using private members.

```csharp
// Solution 1
class Laptop
{
    private string _os;
    public Laptop(string os)
    {
        _os = os;
    }

    public string GetOs()
    {
        return _os;
    }
}
var Laptop = new Laptop("macOs");
Console.WriteLine(Laptop.GetOs());

// Solution 2
class Laptop
{
    public string Os { get; } // read-only property
    public Laptop(string os)
    {
        Os = os; // initialize read-only property
    }
}
var Laptop = new Laptop("macOs");
Console.WriteLine(Laptop.Os);
```
Analysis of the first solution:
- The Os property was changed to become a private member (_os).
- To prevent direct modifications from outside the class, I removed Os public setter method.
- To provide external access to the value of Os, I introduced GetOs(), a public getter method.
- The value supplied as a parameter is used to initialize the private member _os in the constructor.

With these modifications, the Os property can only be accessed through the GetOs() method, and its value cannot be modified directly. This ensures better encapsulation and control over the class's internal state.

However, it's worth noting that in a real-world scenario, using properties with private setters and public getters (read-only properties) might be a more common and idiomatic approach. If that's the case, the Laptop class would look like the second solution. In this version, the Os property has a private setter, making it read-only. The value can still be set in the constructor, but it cannot be modified thereafter.

### 4. How about your opinion..?

```csharp
using System; 
using System.Collections.Generic;

namespace MemoryLeakExample 
{ 
    class Program 
    { 
        static void Main(string[] args) 
        { 
            var myList = new List(); 
            while (true) 
            { 
                // populate list with 1000 integers 
                for (int i = 0; i < 1000; i++) 
                { 
                    myList.Add(new Product(Guid.NewGuid().ToString(), i)); 
                } 
                // do something with the list object 
                Console.WriteLine(myList.Count); 
            }
        } 
    } 
    
    class Product 
    { 
        public Product(string sku, decimal price) 
        { 
            SKU = sku; 
            Price = price; 
        } 

        public string SKU { get; set; } 
        public decimal Price { get; set; } 
    } 
}
```
**Key:** modifications by using private members.

```csharp
// Solution
using System;
using System.Collections.Generic;

namespace MemoryLeakExample
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var myList = new List<Product>(); // Create a new list in each iteration

                // populate list with 1000 integers
                for (int i = 0; i < 1000; i++)
                {
                    myList.Add(new Product(Guid.NewGuid().ToString(), i));
                }

                // do something with the list object
                Console.WriteLine(myList.Count);

                myList.Clear(); // Clear the list to release memory
            }
        }
    }

    class Product
    {
        public Product(string sku, decimal price)
        {
            SKU = sku;
            Price = price;
        }

        public string SKU { get; set; }
        public decimal Price { get; set; }
    }
}
```
In the revised code;
- I made sure to create a list named myList inside the while loop, for each iteration.
- Once I finish using myList I use the Clear() method to empty the list and free up the memory allocated to its objects.
Clearing the list at the end of each loop prevents a buildup of objects, in memory effectively resolving the memory leak problem.

### 5. How about your opinion..?

```csharp
using System; 
namespace MemoryLeakExample 
{ 
    class Program 
    { 
        static void Main(string[] args) 
        { 
            var publisher = new EventPublisher(); 
            while (true) 
            { 
                var subscriber = new EventSubscriber(publisher); 
                // do something with the publisher and subscriber objects 
            } 
        } 
        
        class EventPublisher 
        { 
            public event EventHandler MyEvent; 
            public void RaiseEvent() 
            { 
                MyEvent?.Invoke(this, EventArgs.Empty); 
            } 
        } 
        
        class EventSubscriber 
        { 
            public EventSubscriber(EventPublisher publisher) 
            { 
                publisher.MyEvent += OnMyEvent; 
            } 

            private void OnMyEvent(object sender, EventArgs e) 
            { 
                Console.WriteLine("MyEvent raised"); 
            } 
        } 
    } 
} 
```
**Key:** event handlers.

```csharp
// Solution
using System;

namespace MemoryLeakExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var publisher = new EventPublisher();
            while (true)
            {
                using (var subscriber = new EventSubscriber(publisher))
                {
                    // do something with the publisher and subscriber objects 
                }
            }
        }
    }

    class EventPublisher
    {
        public event EventHandler MyEvent;

        public void RaiseEvent()
        {
            MyEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    class EventSubscriber : IDisposable
    {
        private readonly EventPublisher _publisher;

        public EventSubscriber(EventPublisher publisher)
        {
            _publisher = publisher;
            _publisher.MyEvent += OnMyEvent;
        }

        private void OnMyEvent(object sender, EventArgs e)
        {
            Console.WriteLine("MyEvent raised");
        }

        public void Dispose()
        {
            _publisher.MyEvent -= OnMyEvent; // Unsubscribe from the event
        }
    }
}
```
In this corrected code:
- I've updated the EventSubscriber class to include the IDisposable interface.
- In the Main method, I've added a using statement around the EventSubscriber object instantiation. This makes sure that when the EventSubscriber object exits scope, the Dispose() method is performed automatically.
- The EventSubscriber class's dispose() method releases the reference to the EventSubscriber object and permits it to be garbage collected by unsubscribing from the EventPublisher's MyEvent event.

### 6. How about your opinion..?

```csharp
using System; 
using System.Collections.Generic; 
namespace MemoryLeakExample 
{ 
    class Program 
    { 
        static void Main(string[] args) 
        { 
            var rootNode = new TreeNode(); 
            while (true) 
            { 
                // create a new subtree of 10000 nodes 
                var newNode = new TreeNode(); 
                for (int i = 0; i < 10000; i++) 
                { 
                    var childNode = new TreeNode(); 
                    newNode.AddChild(childNode); 
                } 
                rootNode.AddChild(newNode); 
            } 
        } 
    } 

    class TreeNode 
    { 
        private readonly List<TreeNode> _children = new List<TreeNode>(); 
        public void AddChild(TreeNode child) 
        { 
            _children.Add(child); 
        } 
    } 
}
```
**Key:** Large object graphs.

```csharp
// Solution
using System;
using System.Collections.Generic;

namespace MemoryLeakExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootNode = new TreeNode();
            while (true)
            {
                // Create a new subtree of 10000 nodes
                var newNode = new TreeNode();
                for (int i = 0; i < 10000; i++)
                {
                    var childNode = new TreeNode();
                    newNode.AddChild(childNode);
                }
                rootNode.AddChild(newNode);

                // Prune old subtrees if the number of children exceeds a threshold
                if (rootNode.ChildCount > 100)
                {
                    rootNode.PruneOldSubtrees();
                }
            }
        }
    }

    class TreeNode
    {
        private readonly List<TreeNode> _children = new List<TreeNode>();

        public int ChildCount => _children.Count;

        public void AddChild(TreeNode child)
        {
            _children.Add(child);
        }

        public void PruneOldSubtrees()
        {
            // Prune old subtrees by removing the first 50% of children
            int pruneCount = _children.Count / 2;
            _children.RemoveRange(0, pruneCount);
        }
    }
}
```
In this corrected code:
- I modified the TreeNode class to include a function called PruneOldSubtrees() that eliminates outdated subtrees from the children list.
- In the Main method, I determine whether the number of children in the new subtree surpasses a predetermined threshold (e.g., 100) after adding it to the root node. I remove outdated subtrees from the root node if it happens.
- The PruneOldSubtrees() function effectively prunes old subtrees and frees up memory by removing the first 50% of children from the _children list.
By preventing the growth of a big object graph, this basic pruning mechanism helps to mitigate the memory leak problem.

### 7. How about your opinion..?

```csharp
using System; 
using System.Collections.Generic; 

class Cache 
{ 
    private static Dictionary<int, object> _cache = new Dictionary<int, object>(); 
    public static void Add(int key, object value) 
    { 
        _cache.Add(key, value); 
    } 

    public static object Get(int key) 
    { 
        return _cache[key]; 
    } 
} 

class Program 
{ 
    static void Main(string[] args) 
    { 
        for (int i = 0; i < 1000000; i++) 
        { 
            Cache.Add(i, new object()); 
        }
        Console.WriteLine("Cache populated"); 
        Console.ReadLine(); 
    } 
}
```
**Key:** Improper caching

```csharp
// Solution
using System;
using System.Collections.Generic;

class LRUCache<TKey, TValue>
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<(TKey Key, TValue Value)>> _cacheMap;
    private readonly LinkedList<(TKey Key, TValue Value)> _cacheList;

    public LRUCache(int capacity)
    {
        _capacity = capacity;
        _cacheMap = new Dictionary<TKey, LinkedListNode<(TKey, TValue)>>();
        _cacheList = new LinkedList<(TKey, TValue)>();
    }

    public void Add(TKey key, TValue value)
    {
        if (_cacheMap.ContainsKey(key))
        {
            _cacheList.Remove(_cacheMap[key]);
        }
        else if (_cacheList.Count >= _capacity)
        {
            var nodeToRemove = _cacheList.Last;
            _cacheList.RemoveLast();
            _cacheMap.Remove(nodeToRemove.Value.Key);
        }

        var newNode = _cacheList.AddFirst((key, value));
        _cacheMap[key] = newNode;
    }

    public bool TryGet(TKey key, out TValue value)
    {
        if (_cacheMap.TryGetValue(key, out var node))
        {
            _cacheList.Remove(node);
            _cacheList.AddFirst(node);
            value = node.Value.Value;
            return true;
        }

        value = default;
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        LRUCache<int, object> cache = new LRUCache<int, object>(1000);

        for (int i = 0; i < 1000000; i++)
        {
            cache.Add(i, new object());
        }

        Console.WriteLine("Cache populated");
        Console.ReadLine();
    }
}
```
In this corrected code:
- I've created a generic key and value type LRU cache (LRUCache) class.
- The LRUCache class tracks the order of access using a LinkedList (_cacheList) and stores key-value pairs in a Dictionary (_cacheMap).
- I make sure the cache hasn't filled up before adding a new item (Add method). If that's the case, I add the new item after removing the least recently used one.
- I move the accessible item to the front of the list to adjust the order of access when I retrieve an item from the cache (TryGet method).
- This LRU cache implementation prevents excessive memory usage by making sure the cache doesn't grow infinitely and by evicting old items to create place for new ones.