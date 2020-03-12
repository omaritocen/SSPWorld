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
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCourseByIdAsync(string id);
        Task<List<Course>> GetEnrolledCourses();
    }

    public class CourseService : BaseService, ICourseService
    {
        private readonly HttpClient _client;
        public CourseService()
        {
            _client = new HttpClient();
            var token = Application.Current.Properties["token"];
            _client.DefaultRequestHeaders.Add("x-auth-token", token.ToString());
        }



        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            var url = string.Concat(URL, "courses");
            var uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var courses = JsonConvert.DeserializeObject<List<Course>>(result);
                    return courses;
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle the server error
            }

            return null;
        }

        public async Task<Course> GetCourseByIdAsync(string id)
        {
            var url = string.Concat(URL, "courses/" + id);
            var uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var course = JsonConvert.DeserializeObject<Course>(result);
                    return course;
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle the server error
            }

            return null;
        }

        public async Task<List<Course>> GetEnrolledCourses()
        {
            var url = string.Concat(URL, "courses/getEnrolledCourses");
            var uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var courses = JsonConvert.DeserializeObject<List<Course>>(result);
                    return courses;
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
