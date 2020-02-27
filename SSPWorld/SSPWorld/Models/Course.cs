using System;
using System.Collections.Generic;
using System.Text;

namespace SSPWorld.Models
{
    public enum Term { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Last}
    public enum CourseType { Core, Elective, Humanity }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public CourseType CourseType { get; set; }
        public Term CourseTerm { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public IEnumerable<Update> Updates { get; set; }
    }
}
