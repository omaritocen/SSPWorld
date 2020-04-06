using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Windows.UI.Xaml;
using Xamarin.Forms;


namespace SSPWorld.Services
{
    public class BaseService
    {
        protected HttpClient HttpClient;

        public BaseService()
        {
            HttpClient = HttpClientSingleton.GetInstance();
        }

        public string URL = SetUrl();

        private static string SetUrl()
        {
            var url = "";
            if (Device.RuntimePlatform == Device.Android)
                url = "http://10.0.2.2:3000/api/";
            else if (Device.RuntimePlatform == Device.iOS)
                url = "http://localhost:3000/api/";
            else if (Device.RuntimePlatform == Device.UWP)
                url = "http://localhost:3000/api/";
            return url;
        }
    }
}
