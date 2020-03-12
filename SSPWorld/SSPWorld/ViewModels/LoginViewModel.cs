using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using SSPWorld.Services;
using SSPWorld.Views;
using Xamarin.Forms;

namespace SSPWorld.ViewModels
{
    public class LoginViewModel
    {
        private readonly INavigation _navigation;
        private readonly UserService _userService = new UserService();
        public string SSPId { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; private set; }

        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
            BindCommands();
        }

        private void BindCommands()
        {
            LoginCommand = new Command(async c => await Login());
        }

        private async Task Login()
        {
            using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
            {
                var token = await _userService.Login(int.Parse(SSPId), Password);


                if (token != null)
                {
                    Application.Current.Properties["SSPID"] = SSPId;
                    Application.Current.Properties["token"] = token;
                    await _navigation.PushAsync(new HomePage());
                }

                else
                    UserDialogs.Instance.Alert("Wrong ID or Password");
            }
        
        }
    }
}
