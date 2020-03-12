using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSPWorld.Models;
using Xamarin.Forms;

namespace SSPWorld.Services
{

    public interface IStudentService
    {
        Task<Student> GetStudentProfileAsync();
    }

    public class StudentService : BaseService, IStudentService
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<Student> GetStudentProfileAsync()
        {
            var url = string.Concat(URL, "students");
            var uri = new Uri(string.Format(url, string.Empty));
            var token = Application.Current.Properties["token"];
            _client.DefaultRequestHeaders.Add("x-auth-token", token.ToString());
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var student = JsonConvert.DeserializeObject<Student>(result);
                    return student;
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
