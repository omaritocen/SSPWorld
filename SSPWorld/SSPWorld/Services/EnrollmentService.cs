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
    public interface IEnrollmentService
    {
        Task AddNewEnrollmentAsync(string courseId);
        Task<bool> DeleteEnrollmentAsync(string courseId);
        Task<bool> IsEnrolledAsync(string courseId);
    }

    public class EnrollmentService : BaseService, IEnrollmentService
    {

        public async Task AddNewEnrollmentAsync(string courseId)
        {
            var url = string.Concat(URL, "enrollments");
            var uri = new Uri(string.Format(url, string.Empty));

            var data = new { _courseId = courseId};
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var enrollment = JsonConvert.DeserializeObject<Enrollment>(result);
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle the server error
            }
        }

        public async Task<bool> DeleteEnrollmentAsync(string courseId)
        {
            var url = string.Concat(URL, "enrollments/deleteByCourseId/" + courseId);
            var uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var enrollment = JsonConvert.DeserializeObject<Enrollment>(result);
                    if (enrollment != null) return true;
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle the server error
            }

            return false;
        }

        public async Task<bool> IsEnrolledAsync(string courseId)
        {
            var url = string.Concat(URL, "enrollments/isEnrolled/" + courseId);
            var uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var enrollment = JsonConvert.DeserializeObject<Enrollment>(result);
                    if (enrollment != null) return true;
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle the server error
            }

            return false;
        }
    }
}
