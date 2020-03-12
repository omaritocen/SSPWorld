using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SSPWorld.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailsPage : ContentPage
    {
        public CourseDetailsPage(string studentId)
        {
            InitializeComponent();
            BindingContext = new CourseDetailsViewModel(studentId, Navigation);
        }
    }
}