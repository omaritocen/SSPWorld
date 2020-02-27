using System;
using System.Collections.Generic;
using System.Text;

namespace SSPWorld.Models
{
    public class CourseGrouping : List<Course>
    {
        public string Title { get; set; }
        public string ShortName { get; set; }

        public CourseGrouping(string title, string shortName)
        {
            Title = title;
            ShortName = shortName;
        }
    }
}
