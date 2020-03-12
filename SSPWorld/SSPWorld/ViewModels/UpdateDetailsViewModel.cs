using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SSPWorld.Models;
using SSPWorld.Repositories;
using Xamarin.Forms;

namespace SSPWorld.ViewModels
{
    public class UpdateDetailsViewModel : BaseViewModel
    {
        private readonly IUpdateRepository _updatesRepository = new UpdatesRepository();
        private readonly CourseRepository _courseRepository = new CourseRepository();
        public Update Update { get; set; }
        public string CourseName { get; set; }
        public string DaysLeft { get; set; }

        public UpdateDetailsViewModel(string updateId)
        {
            GetUpdate(updateId);
            BindCommands();
        }

        private void BindCommands()
        {
            
        }

        private void GetUpdate(string updateId)
        {
            Task.Run(async () =>
            {
                Update = await _updatesRepository.GetUpdateById(updateId);
                var course = await _courseRepository.GetCourseById(Update.CourseId);
                CourseName = course.Name;
            }).Wait();

            CalculateDeadLine();
        }

        private void CalculateDeadLine()
        {
            var dateNow = DateTime.Now;
            var compare = DateTime.Compare(dateNow, Update.Deadline);

            if (compare < 0)
                DaysLeft = (Update.Deadline.Date - dateNow.Date).Days.ToString();
            else
                DaysLeft = "THE DEADLINE HAS ALREADY PASSED!";
        }

    }
}
