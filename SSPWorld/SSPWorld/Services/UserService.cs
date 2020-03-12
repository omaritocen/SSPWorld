using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SSPWorld.Models;

namespace SSPWorld.Services
{
    public class UserService : BaseService
    {
        private HttpClient _client = new HttpClient();

        public async Task<string> Login(int id, string password)
        {
            var url = string.Concat(URL, "auth");
            var uri = new Uri(string.Format(url, string.Empty));

            var loginData = new {sspID = id, password = password};
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<TokenDto>(result);
                    return token.Token;
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle the server error
            }

            return null;
        }
    }
}
