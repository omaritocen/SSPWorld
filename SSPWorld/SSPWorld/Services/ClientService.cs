using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SSPWorld.Services
{
    public class ClientService
    {
        public static HttpClient HttpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(10)
        };
    }
}
