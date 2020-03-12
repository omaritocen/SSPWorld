using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class ProfileViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IStudentRepository _studentRepository = new StudentRepository();
        private readonly ICourseRepository _courseRepository = new CourseRepository();

        private ObservableCollection<Course> _courses = new ObservableCollection<Course>();

        public ObservableCollection<Course> Courses
        {
            get => _courses;
            set
            {
                if (_courses == value) return;

                _courses = value;
                OnPropertyChanged();
            }
        }

        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get => _selectedCourse;

            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
            }
        }

        public Student Student { get; set; } = new Student();

        public string FullName => string.Format("{0} {1}", Student.FirstName, Student.LastName);

        public ICommand ItemTappedCommand { get; private set; }

        public ProfileViewModel(INavigation navigation)
        {
            _navigation = navigation;
            BindCommands();
            CheckForNewSubjects();
            SetProfile();
        }


        private void BindCommands()
        {
            ItemTappedCommand = new Command(ItemTapped);
        }

        private void CheckForNewSubjects()
        {
            MessagingCenter.Subscribe<CourseDetailsViewModel, string>
            (this, "EnrollmentsChanged",
                async (obj, arg) =>
                {
                    await PopulateSubjects();
                });
        }

        private void SetProfile()
        {
            Task.Run(async () =>
            {
                var student = await _studentRepository.GetStudentProfile();
                Student = student;
                await PopulateSubjects();

            }).Wait();
        }

        private async Task PopulateSubjects()
        {
            _courses.Clear();

            var courses = await _courseRepository.GetEnrolledCourses();
            if (courses == null) return;
            foreach (var course in courses)
            {
                _courses.Add(course);
            }
        }


        private async void ItemTapped()
        {
            await _navigation.PushModalAsync(new CourseDetailsPage(SelectedCourse.Id));
            SelectedCourse = null;
        }

    }
}
