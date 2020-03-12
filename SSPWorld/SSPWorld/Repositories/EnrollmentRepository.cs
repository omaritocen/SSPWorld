using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;
using SSPWorld.Services;

namespace SSPWorld.Repositories
{
    public interface IEnrollmentRepository
    {
        Task AddNewEnrollment(string courseId);
        Task DeleteEnrollment(string courseId);
        Task<bool> IsEnrolled(string courseId);
    }

    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly IEnrollmentService _enrollmentService = new EnrollmentService();

        public async Task AddNewEnrollment(string courseId)
        {
            await _enrollmentService.AddNewEnrollmentAsync(courseId);
        }

        public async Task DeleteEnrollment(string courseId)
        {
            await _enrollmentService.DeleteEnrollmentAsync(courseId);
        }

        public async Task<bool> IsEnrolled(string courseId)
        {
            return await _enrollmentService.IsEnrolledAsync(courseId);
        }
    }
}
