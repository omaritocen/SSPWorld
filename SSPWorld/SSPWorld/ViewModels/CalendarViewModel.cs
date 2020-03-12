using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;
using SSPWorld.Repositories;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;

namespace SSPWorld.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        private readonly IUpdateRepository _updatesRepository = new UpdatesRepository();
        public DateTime DateNow { get; set; } = DateTime.Now;

        public EventCollection Events { get; private set; }

        public CalendarViewModel()
        {
            InitCalendar();
        }

        private void InitCalendar()
        {
            var updates = new List<Update>();
            Task.Run(async () =>
            {
                updates = await _updatesRepository.GetEnrolledUpdates();
            }).Wait();
            

            Dictionary<DateTime, List<Update>> groupedUpdates =
                updates
                    .GroupBy(k => k.Deadline)
                    .OrderByDescending(k => k.Key)
                    .ToDictionary(k => k.Key, v => v.OrderByDescending(x => x.Deadline).ToList());

            Events = new EventCollection();
            foreach (var date in groupedUpdates)
            {
                var key = date.Key;
                Events.Add(key, groupedUpdates[key]);
            }
        }
    }
}
