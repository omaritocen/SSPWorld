using System;
using System.Collections.Generic;
using System.Text;

namespace SSPWorld.Models
{
    public class Update
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}
