using System;
using System.Collections.Generic;
using System.Text;

namespace SSPWorld.Models
{

    public enum Year { Prep, First, Second, Third, Senior }
    public enum Department { General, CCE, EME, CAE, BME, GPE, OCE }

    public class Student
    {
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Year Year { get; set; }
        public Department Department { get; set; }

    }
}
