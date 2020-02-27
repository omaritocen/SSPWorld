using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;
using SSPWorld.Services;

namespace SSPWorld.Repositories
{
    public class UpdatesRepository
    {
        private readonly UpdatesService _updatesService = new UpdatesService();
        private readonly EnrollmentRepository _enrollmentRepository = new EnrollmentRepository();

        public async Task<Update> GetUpdateById(int id)
        {
            return await _updatesService.GetUpdateByIdAsync(id);
        }

        public async Task<IEnumerable<Update>> GetUpdatesByCourseId(int courseId)
        {
            return await _updatesService.GetStudentUpdatesByCourseIdAsync(courseId);
        }

        public async Task<IEnumerable<Update>> GetUpdatesBySSPId(int SSPId)
        {
            var enrollments = await _enrollmentRepository.GetStudentEnrollmentsById(SSPId);
            var coursesId = enrollments.Where(x => x.StudentId == SSPId)
                                       .Select(x => x.CourseId);

            var updates = new List<Update>();
            foreach (var courseId in coursesId)
            {
                var pack = await GetUpdatesByCourseId(courseId);
                updates.AddRange(pack);
            }

            return updates;
        }
    }
}
