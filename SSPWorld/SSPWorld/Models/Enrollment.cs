using System;
using System.Collections.Generic;
using System.Text;

namespace SSPWorld.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
