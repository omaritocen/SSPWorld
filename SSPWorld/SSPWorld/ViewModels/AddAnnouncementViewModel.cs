using System;
using System.Windows.Input;
using SSPWorld.Models;
using Xamarin.Forms;

namespace SSPWorld.ViewModels
{
    public class AddAnnouncementViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }

        public ICommand SubmitUpdateCommand { get; private set; }

        public AddAnnouncementViewModel()
        {
            BindCommands();
        }

        private void BindCommands()
        {
            SubmitUpdateCommand = new Command(SubmitUpdate);
        }

        private void SubmitUpdate()
        {
            var update = new Update()
            {
                Title = this.Title,
                Body = this.Body,
                StartDate = this.StartDate,
                Deadline = this.Deadline
            };
        }
    }
}
