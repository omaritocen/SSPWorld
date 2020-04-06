using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SSPWorld.Services
{
    public class HttpClientSingleton
    {
        private HttpClientSingleton()
        {
            
        }

        private static HttpClient _httpClient;

        public static HttpClient GetInstance()
        {
            return _httpClient ?? (_httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5)
            });
        }

        public static void SetAccessToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Add("x-auth-token", token);
        }
    }
}
