using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Newtonsoft.Json;
using SSPWorld.Models;
using Xamarin.Forms;

namespace SSPWorld.Services
{

    public interface IStudentService
    {
        Task<Student> GetStudentProfileAsync();
        Task<string> CreateStudentProfileAsync(Student student);
    }

    public class StudentService : BaseService, IStudentService
    {

        public async Task<Student> GetStudentProfileAsync()
        {
            var url = string.Concat(URL, "students");
            var uri = new Uri(string.Format(url, string.Empty));

            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.GetAsync(uri);
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

        public async Task<string> CreateStudentProfileAsync(Student student)
        {
            var url = string.Concat(URL, "students");
            var uri = new Uri(string.Format(url, string.Empty));

            var json = JsonConvert.SerializeObject(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                    return "Success";

                var result = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorModel>(result);
                return error.Error;
                
            }
            catch (Exception ex)
            {
                // TODO: Handle the server error
            }

            return null;
        }
    }
}
