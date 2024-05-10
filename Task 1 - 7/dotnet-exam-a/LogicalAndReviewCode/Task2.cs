using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_exam_a.Models.Task2;

namespace dotnet_exam_a.LogicalAndReviewCode
{
    public class Task2
    {
        public static void Execute()
        {
            Console.WriteLine("Result of Task 2:");

            Task2 task2 = new Task2();
            var info = task2.GetInfo();

            // Menampilkan nilai path dan name
            Console.WriteLine($"Path: {info.Path}");
            Console.WriteLine($"Name: {info.Name}");
        }

        public (string Path, string Name) GetInfo()
        {
            var application = new ApplicationInfo
            {
                Path = "C:/apps/",
                Name = "Shield.exe"
            };
            return (application.Path, application.Name);
        }
    }
}