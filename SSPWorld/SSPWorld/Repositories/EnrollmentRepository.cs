using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;
using SSPWorld.Services;

namespace SSPWorld.Repositories
{
    public class EnrollmentRepository
    {
        private readonly EnrollmentService _enrollmentService = new EnrollmentService();

        public async Task<IEnumerable<Enrollment>> GetEnrollments()
        {
            return await _enrollmentService.GetEnrollmentsAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetStudentEnrollmentsById(int id)
        {
            return await _enrollmentService.GetEnrollmentsByStudentId(id);
        }

        public async Task<bool> IsEnrolled(int studentId, int courseId)
        {
            var enrollments = await GetStudentEnrollmentsById(studentId);
            foreach (var enrollment in enrollments)
            {
                if (enrollment.CourseId == courseId)
                    return true;
            }

            return false;
        }

        public async Task AddNewEnrollment(Enrollment enrollment)
        {
            await _enrollmentService.AddNewEnrollmentAsync(enrollment);
        }

        public async Task DeleteEnrollment(Enrollment enrollment)
        {
            await _enrollmentService.DeleteEnrollmentAsync(enrollment);
        }

    }
}
