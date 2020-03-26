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
    public interface IUpdatesService 
    {
        Task<List<Update>> GetEnrolledUpdatesAsync();
        Task<Update> GetUpdateByIdAsync(string id);

        Task<List<Update>> GetStudentUpdatesByCourseIdAsync(string courseId);
    }

    public class UpdatesService : BaseService, IUpdatesService
    {

        public async Task<List<Update>> GetEnrolledUpdatesAsync()
        {
            var url = string.Concat(URL, "updates/getStudentUpdates");
            var uri = new Uri(string.Format(url, string.Empty));

            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var updates = JsonConvert.DeserializeObject<List<Update>>(result);
                    return updates;
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle the server error
            }

            return null;
        }

        public async Task<Update> GetUpdateByIdAsync(string id)
        {
            var url = string.Concat(URL, "updates/" + id);
            var uri = new Uri(string.Format(url, string.Empty));

            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var update = JsonConvert.DeserializeObject<Update>(result);
                    return update;
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle the server error
            }

            return null;
        }


        public async Task<List<Update>> GetStudentUpdatesByCourseIdAsync(string courseId)
        {
            var url = string.Concat(URL, "updates/getUpdatesByCourseId/" + courseId);
            var uri = new Uri(string.Format(url, string.Empty));

            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var updates = JsonConvert.DeserializeObject<List<Update>>(result);
                    return updates;
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
