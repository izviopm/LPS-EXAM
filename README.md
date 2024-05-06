# A. Logical and Review Code
#### 1. How about your opinion..?
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
// Solution
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
- To prevent direct modifications from outside the class, we eliminated the public setter function for Os.
- To provide external access to the value of Os, we introduced a public getter function called GetOs().
- The value supplied as a parameter is used to initialize the private member _os in the constructor.

With these modifications, the Os property can only be accessed through the GetOs() method, and its value cannot be modified directly. This ensures better encapsulation and control over the class's internal state.

However, it's worth noting that in a real-world scenario, using properties with private setters and public getters (read-only properties) might be a more common and idiomatic approach. If that's the case, the Laptop class would look like the second solution. In this version, the Os property has a private setter, making it read-only. The value can still be set in the constructor, but it cannot be modified thereafter.


