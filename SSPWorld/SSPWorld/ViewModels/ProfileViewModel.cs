using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SSPWorld.Models;
using SSPWorld.Repositories;
using SSPWorld.Views;
using Xamarin.Forms;

namespace SSPWorld.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly StudentRepository _studentRepository = new StudentRepository();
        private readonly EnrollmentRepository _enrollmentRepository = new EnrollmentRepository();
        private readonly CourseRepository _courseRepository = new CourseRepository();

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
            SetProfile();
            BindCommands();
            CheckForNewSubjects();
        }

        private void CheckForNewSubjects()
        {
            MessagingCenter.Subscribe<CourseDetailsViewModel, Enrollment>
            (this, "EnrollmentsChanged",
                async (obj, arg) =>
                {
                    await PopulateSubjects(arg);
                });
        }

        private async void SetProfile()
        {
            var id = Application.Current.Properties["SSPID"];
            var student = await _studentRepository.GetStudentBySSPId(Convert.ToInt32(id));
            Student = student;
            await PopulateSubjects();
         
        }

        private async Task PopulateSubjects(Enrollment enrollment = null)
        {
            _courses.Clear();
            var enrollments = await
                _enrollmentRepository.GetStudentEnrollmentsById(Student.SSPId);
            var coursesIds = enrollments.Select(x => x.CourseId);

            foreach (var id in coursesIds)
            {
                var course = await _courseRepository.GetCourseById(id);
                _courses.Add(course);
            }

            // REDUNDANT SOLUTION
            if (enrollment != null)
            {
                var course = await _courseRepository.GetCourseById(enrollment.CourseId);
                _courses.Add(course);
            }
        }

        private void BindCommands()
        {
            ItemTappedCommand = new Command(ItemTapped);
        }

        private async void ItemTapped()
        {
            await _navigation.PushModalAsync(new CourseDetailsPage(SelectedCourse.Id));
            SelectedCourse = null;
        }
    }
}
