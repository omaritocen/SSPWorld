using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;
using SSPWorld.Services;

namespace SSPWorld.Repositories
{
    public interface IUpdateRepository
    {
        Task<List<Update>> GetEnrolledUpdates();
        Task<List<Update>> GetUpdatesByCourseId(string courseId);
        Task<Update> GetUpdateById(string updateId);
    }

    public class UpdatesRepository : IUpdateRepository
    {
        private readonly IUpdatesService _updatesService = new UpdatesService();

        public async Task<Update> GetUpdateById(string id)
        {
            return await _updatesService.GetUpdateByIdAsync(id);
        }

        public async Task<List<Update>> GetUpdatesByCourseId(string courseId)
        {
            return await _updatesService.GetStudentUpdatesByCourseIdAsync(courseId);
        }

        public async Task<List<Update>> GetEnrolledUpdates()
        {
            return await _updatesService.GetEnrolledUpdatesAsync();
        }
    }
}
