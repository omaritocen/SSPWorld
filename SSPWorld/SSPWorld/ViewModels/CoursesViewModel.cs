using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;
using SSPWorld.Repositories;
using System.Linq;
using System.Windows.Input;
using SSPWorld.Utilities;
using SSPWorld.Views;
using Xamarin.Forms;

namespace SSPWorld.ViewModels
{
    public class CoursesViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly CourseRepository _courseRepository = new CourseRepository();
        private readonly EnrollmentRepository _enrollmentRepository = new EnrollmentRepository();
        private List<CourseGrouping> _courseGroups = new List<CourseGrouping>();

        private ObservableCollection<CourseGrouping> _courses = new ObservableCollection<CourseGrouping>();

        public ObservableCollection<CourseGrouping> Courses
        {
            get => _courses;
            set
            {
                if (_courses == value) return;

                _courses = value;
                OnPropertyChanged();
            }
        }


        private Term _selectedTerm;
        public Term SelectedTerm
        {
            get => _selectedTerm;
            set
            {
                if (_selectedTerm == value)
                    return;
                _selectedTerm = value;
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

        public List<string> TermsNames => Enum.GetNames(typeof(Term)).Select(b => b.SplitCamelCase()).ToList();

        
        public ICommand SelectedIndexCommand { get; private set; }
        public ICommand RestFilterCommand { get; private set; }
        public ICommand ItemTappedCommand { get; private set; }



        public CoursesViewModel(INavigation navigation)
        {
            _navigation = navigation;
            BindCommands();
            SetCourses();
        }

        private void BindCommands()
        {
            SelectedIndexCommand = new Command(SelectedIndex);
            RestFilterCommand = new Command(async x => await RestFilter());
            ItemTappedCommand = new Command(ItemTapped);
        }

        private void FillAllCourses()
        {
            foreach (var courseGroup in _courseGroups)
            {
                _courses.Add(courseGroup);
            }
        }

        private void SelectedIndex()
        {
            var courses = _courseGroups;
            courses = courses.Where(x => x.Title == SelectedTerm.ToString()).ToList();
            _courses.Clear();
            foreach (var courseGrouping in courses)
            {
                _courses.Add(courseGrouping);
            }
        }

        private async Task RestFilter()
        {
            _courses.Clear();
            FillAllCourses();
        }


        private async void SetCourses()
        {
            await PopulateSubjects();
        }

        private async void ItemTapped()
        {
            await _navigation.PushModalAsync(new CourseDetailsPage(SelectedCourse.Id));
            SelectedCourse = null;
        }

        private async Task PopulateSubjects()
        {
            _courseGroups = await _courseRepository.GetCourseGrouped();
            FillAllCourses();
        }
    }
}
