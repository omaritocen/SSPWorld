using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using SSPWorld.Models;
using SSPWorld.Services;
using SSPWorld.Views;
using Xamarin.Forms;

namespace SSPWorld.ViewModels
{
    public class RegisterViewModel
    {
        private readonly INavigation _navigation;
        private readonly IUserService _userService = new UserService();
        public RegisterModel RegisterModel { get; set; } = new RegisterModel();


        public ICommand RegisterCommand { get; private set; }
        public ICommand GoToLoginCommand { get; private set; }

        public RegisterViewModel(INavigation navigation)
        {
            _navigation = navigation;
            BindCommands();
        }

        private void BindCommands()
        {
            RegisterCommand = new Command(async x => await Register());
            GoToLoginCommand = new Command(async x=> await GoToLogin());
        }

        private async Task GoToLogin()
        {
            await _navigation.PushAsync(new LoginPage());
        }

        private async Task Register()
        {
            if (RegisterModel.Password != RegisterModel.ConfirmPassword)
            {
                UserDialogs.Instance.Alert("Passwords does not match"); 
                return;
            }

            var result = await _userService.Register(RegisterModel);
            if (result == "success")
            {
                UserDialogs.Instance.Alert("Registered successfully");
                await _navigation.PushAsync(new LoginPage());
            }
            else if (!string.IsNullOrEmpty(result))
            {
                UserDialogs.Instance.Alert("Error: " + result);
            }
            else
            {
                UserDialogs.Instance.Alert("There's a problem in server");
            }
        }
    }
}
