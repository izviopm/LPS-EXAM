using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_exam_a.Models.Task3;

namespace dotnet_exam_a.LogicalAndReviewCode
{
    public class Task3
    {
        public static void Execute()
        {
            Console.WriteLine("Result of Task 3:");
            var Laptop = new Laptop("macOs");
            Console.WriteLine(Laptop.Os);
        }
    }
}