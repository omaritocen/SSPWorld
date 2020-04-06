using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SSPWorld.Annotations;
using SSPWorld.Models;

namespace SSPWorld.Services
{
    public interface IUserService
    {
        Task<string> Login(int id, string password);
        Task<string> Register(RegisterModel registerModel);
    }
    public class UserService : BaseService, IUserService
    {

        [ItemCanBeNull]
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
                response = await HttpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<TokenDto>(result);
                    HttpClientSingleton.SetAccessToken(token.Token);
                    return token.Token;
                }

                var error = "Error: Wrong ID or Password";
                return error;

            }
            catch (Exception ex)
            {
                var error = "Error: Connection timed out, Check your internet";
                return error;
            }

        }

        public async Task<string> Register(RegisterModel registerModel)
        {
            var url = string.Concat(URL, "users");
            var uri = new Uri(string.Format(url, string.Empty));

            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return "success";
                }

                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                return errorModel.Error;

            }
            catch (Exception ex)
            {
                // TODO: Handle the server error
            }

            return null;
        }
    }
}
