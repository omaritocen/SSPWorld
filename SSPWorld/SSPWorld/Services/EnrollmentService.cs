using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;

namespace SSPWorld.Services
{
    public class EnrollmentService
    {
        private List<Enrollment> _enrollments = new List<Enrollment>()
        {
            new Enrollment
            {
                Id = 1,
                StudentId = 6294,
                CourseId = 2
            },
            new Enrollment
            {
                Id = 2,
                StudentId = 6294,
                CourseId = 4
            },

            new Enrollment
            {
                Id = 3,
                StudentId = 6206,
                CourseId = 3
            },

            new Enrollment
            {
                Id = 4,
                StudentId = 5201,
                CourseId = 1
            },
        };

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsAsync()
        {
            return _enrollments;
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentId(int id)
        {
            return _enrollments.Where(x => x.StudentId == id).ToList();
        }

        public async Task AddNewEnrollmentAsync(Enrollment enrollment)
        {
            _enrollments.Add(enrollment);
        }

        public async Task DeleteEnrollmentAsync(Enrollment enrollment)
        {
            _enrollments.Remove(enrollment);
        }
    }
}
