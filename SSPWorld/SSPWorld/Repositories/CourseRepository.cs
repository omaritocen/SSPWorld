using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;
using SSPWorld.Services;

namespace SSPWorld.Repositories
{
    public class CourseRepository
    {
        private readonly CourseService _courseService = new CourseService();

        public async Task<Course> GetCourseById(int id)
        {
            return await _courseService.GetCourseByIdAsync(id);

        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _courseService.GetCoursesAsync();
        }

        public async Task<List<CourseGrouping>> GetCourseGrouped()
        {
            var courses = await GetCourses();
            var allCoursesGrouped = new List<CourseGrouping>();
            foreach (string name in Enum.GetNames(typeof(Term)))
            {
                var grouping = new CourseGrouping(name, name);
                grouping.AddRange(courses.Where(x => x.CourseTerm.ToString() == name));
                allCoursesGrouped.Add(grouping);
            }

            return allCoursesGrouped;

        }
    }
}
