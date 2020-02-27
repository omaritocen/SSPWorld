using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;

namespace SSPWorld.Services
{
    public class CourseService
    {
        private IEnumerable<Course> _courses = new List<Course>()
        {
            new Course()
            {
                Id = 1,
                Name = "Physics I",
                CreditHours = 3,
                CourseTerm = Term.First,
                CourseType = CourseType.Core
            },

            new Course()
            {
                Id = 2,
                Name = "History of Engineering",
                CreditHours = 1,
                CourseTerm = Term.Second,
                CourseType = CourseType.Humanity
            },

            new Course()
            {
                Id = 3,
                Name = "Technical Building",
                CreditHours = 3,
                CourseTerm = Term.Third,
                CourseType = CourseType.Core
            },

            new Course()
            {
                Id = 4,
                Name = "Operating Systems",
                CreditHours = 2,
                CourseTerm = Term.Fifth,
                CourseType = CourseType.Elective
            }


        };


        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return _courses;
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return _courses.FirstOrDefault(x => x.Id == id);
        }
    }
}
