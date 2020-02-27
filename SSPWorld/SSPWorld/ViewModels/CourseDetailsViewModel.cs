using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using SSPWorld.Models;
using SSPWorld.Repositories;
using SSPWorld.Views;
using Xamarin.Forms;

namespace SSPWorld.ViewModels
{
    public class CourseDetailsViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly EnrollmentRepository _enrollmentRepository = new EnrollmentRepository();
        private readonly CourseRepository _courseRepository = new CourseRepository();
        private readonly UpdatesRepository _updatesRepository = new UpdatesRepository();

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

        public Course Course { get; set; }

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

        public CourseDetailsViewModel(int courseId, INavigation navigation)
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

        private async void GetCourse(int courseId)
        {
            Course = await _courseRepository.GetCourseById(courseId);

            var studentId = Application.Current.Properties["SSPID"];
            IsEnrolled = await _enrollmentRepository
                .IsEnrolled(Convert.ToInt32(studentId), courseId);
        }

        private async void GetUpdates()
        {
            var updates = await _updatesRepository.GetUpdatesByCourseId(Course.Id);
            foreach (var update in updates)
            {
                _updates.Add(update);
            }
        }


        private async void Subscribe()
        {       
            var enrollment = new Enrollment
            {
                CourseId = Course.Id,
                StudentId = Convert.ToInt32(Application.Current.Properties["SSPID"])
            };
            if (!IsEnrolled)
                await _enrollmentRepository.AddNewEnrollment(enrollment);
            else
                await _enrollmentRepository.DeleteEnrollment(enrollment);

            MessagingCenter.Send(this, "EnrollmentsChanged", enrollment);

            IsEnrolled = !IsEnrolled;
        }

        private async void ItemTapped()
        {
            await _navigation.PushModalAsync(new UpdateDetailsPage(SelectedUpdate.Id));
            SelectedUpdate = null;
        }
    }
}
