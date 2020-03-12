using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using SSPWorld.Models;
using SSPWorld.Repositories;
using SSPWorld.Views;
using Xamarin.Forms;

namespace SSPWorld.ViewModels
{
    public class CourseDetailsViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IEnrollmentRepository _enrollmentRepository = new EnrollmentRepository();
        private readonly CourseRepository _courseRepository = new CourseRepository();
        private readonly IUpdateRepository _updatesRepository = new UpdatesRepository();

        private ObservableCollection<Update> _updates = new ObservableCollection<Update>();

        public ObservableCollection<Update> Updates
        {
            get => _updates;
            set
            {
                if (_updates == value)
                    return;
                _updates = value;
                OnPropertyChanged();
            }
        }

        public Course Course { get; set; } = new Course();

        private bool _isEnrolled;
        public bool IsEnrolled
        {
            get => _isEnrolled;
            set
            {
                _isEnrolled = value;
                OnPropertyChanged();
            }
        }

        private Update _selectedUpdate;

        public Update SelectedUpdate
        {
            get => _selectedUpdate;
            set
            {
                if (_selectedUpdate == value) return;

                _selectedUpdate = value;
                OnPropertyChanged();
            }
        }

        public ICommand SubscribeCommand { get; private set; }
        public ICommand ItemTappedCommand { get; private set; }

        public CourseDetailsViewModel(string courseId, INavigation navigation)
        {
            _navigation = navigation;
            BindCommands();
            GetCourse(courseId);
            GetUpdates();
        }

        private void BindCommands()
        {
            SubscribeCommand = new Command(Subscribe);
            ItemTappedCommand = new Command(ItemTapped);
        }

        private void GetCourse(string courseId)
        {
            Task.Run(async () =>
            {
                Course = await _courseRepository.GetCourseById(courseId);
                IsEnrolled = await _enrollmentRepository.IsEnrolled(courseId);
            }).Wait();

        }

        private void GetUpdates()
        {
            var updates = new List<Update>();
            Task.Run(async () => { updates = await _updatesRepository.GetUpdatesByCourseId(Course.Id); }).Wait();
            foreach (var update in updates)
            {
                _updates.Add(update);
            }
        }

        private void Subscribe()
        {
            Task.Run(async () =>
            {
                if (!IsEnrolled)
                    await _enrollmentRepository.AddNewEnrollment(Course.Id);
                else
                    await _enrollmentRepository.DeleteEnrollment(Course.Id);
            }).Wait();

            MessagingCenter.Send(this, "EnrollmentsChanged", Course.Id);

            IsEnrolled = !IsEnrolled;
        }

        private async void ItemTapped()
        {
            await _navigation.PushModalAsync(new UpdateDetailsPage(SelectedUpdate.Id));
            SelectedUpdate = null;
        }
    }
}
