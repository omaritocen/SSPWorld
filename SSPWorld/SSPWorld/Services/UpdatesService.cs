using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;

namespace SSPWorld.Services
{
    public interface IUpdatesService
    {
        Task<IEnumerable<Update>> GetUpdatesAsync();
        Task<Update> GetUpdateByIdAsync(int id);
        Task<IEnumerable<Update>> GetStudentUpdatesByCourseIdAsync(int courseId);
    }

    public class UpdatesService : IUpdatesService
    {

        private List<Update> _updates = new List<Update>()
        {
            new Update()
            {
                Id = 1,
                CourseId = 1,
                Title = "Assignment #3",
                Body = "the link for the drive http://drive.google.com",
                StartDate = new DateTime(2020, 1, 9),
                Deadline = new DateTime(2020, 1, 18)
            },
            new Update()
            {
                Id = 2,
                CourseId = 2,
                Title = "History Report",
                Body = "Make a report about the textile (20 Marks Coursework)",
                StartDate = new DateTime(2020, 1, 3),
                Deadline = new DateTime(2020, 1, 28)
            },

            new Update()
            {
                Id = 3,
                CourseId = 1,
                Title = "Lab Handbook",
                Body = "Please make sure to bring your lab handbook as coursework will be put on",
                StartDate = new DateTime(2020, 1, 2),
                Deadline = new DateTime(2020, 1, 11)
            },
            new Update()
            {
                Id = 4,
                CourseId = 3,
                Title = "Get Papers",
                Body = "The Dr is reminding you to get 10 50*70 papers",
                StartDate = new DateTime(2020, 1, 2),
                Deadline = new DateTime(2020, 1, 11)
            }
        };

        public async Task<IEnumerable<Update>> GetUpdatesAsync()
        {
            return _updates;
        }

        public async Task<Update> GetUpdateByIdAsync(int id)
        {
            return _updates.FirstOrDefault(x => x.Id == id);
        }


        public async Task<IEnumerable<Update>> GetStudentUpdatesByCourseIdAsync(int courseId)
        {
            return _updates.Where(x => x.CourseId == courseId).ToList();
        }
    }
}
