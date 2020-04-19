using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;
using SSPWorld.Services;

namespace SSPWorld.Repositories
{
    public interface ICourseRepository
    {
        Task<Course> GetCourseById(string id);
        Task<IEnumerable<Course>> GetCourses();
        Task<List<CourseGrouping>> GetCourseGrouped();
        Task<List<Course>> GetEnrolledCourses();
    }

    public class CourseRepository : ICourseRepository
    {
        private readonly ICourseService _courseService = new CourseService();

        public async Task<Course> GetCourseById(string id)
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

        public async Task<List<Course>> GetEnrolledCourses()
        {
            return await _courseService.GetEnrolledCourses();
        }
    }
}
