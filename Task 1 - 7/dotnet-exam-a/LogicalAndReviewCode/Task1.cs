using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_exam_a.Models.Task1;

namespace dotnet_exam_a.LogicalAndReviewCode
{
    public class Task1
    {
        // Task 1: How about your opinion..?
        public static void Execute()
        {
            Console.WriteLine("Result of Task 1:");

            var application = new Application();
            var shieldLastRun = GetShieldLastRun(application);

            Console.WriteLine(shieldLastRun.HasValue ? shieldLastRun.Value.ToString() : "Shield last run is not available.");
        }

        // Task 1: Helper method
        private static DateTime? GetShieldLastRun(Application application)
        {
            return application?.Protected?.ShieldLastRun;
        }
    }
}