using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_exam_a.Models.Task3
{
    public class Laptop
    {
        public string Os { get; } // read-only property
        public Laptop(string os)
        {
            Os = os; // initialize read-only property
        }
    }
}