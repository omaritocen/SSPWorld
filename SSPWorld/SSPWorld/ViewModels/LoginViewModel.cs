using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using SSPWorld.Repositories;
using SSPWorld.Services;
using SSPWorld.Views;
using Xamarin.Forms;

namespace SSPWorld.ViewModels
{
    public class LoginViewModel
    {
        private readonly INavigation _navigation;
        private readonly UserService _userService = new UserService();
        private readonly IStudentRepository _studentRepository = new StudentRepository();
        public string SSPId { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; private set; }
        public ICommand GoToRegisterCommand { get; private set; }

        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
            BindCommands();
        }

        private void BindCommands()
        {
            LoginCommand = new Command(async c => await Login());
            GoToRegisterCommand = new Command(async c => await GoToRegister());
        }

        private async Task GoToRegister()
        {
            await _navigation.PushAsync(new RegisterPage());
        }

        private async Task Login()
        {
            using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
            {
                var result = await _userService.Login(int.Parse(SSPId), Password);

                if (result != null && result.Contains("Error"))
                {
                    UserDialogs.Instance.Alert(result);
                    return;
                }

                Application.Current.Properties["SSPID"] = SSPId;

                await Application.Current.SavePropertiesAsync();

                var profile = await _studentRepository.GetStudentProfile();
                if (profile == null)
                {
                    await _navigation.PushAsync(new CreateStudentProfilePage());
                }
                else 
                    App.Current.MainPage = new NavigationPage(new HomePage());
            }
        
        
        }
    }
}
