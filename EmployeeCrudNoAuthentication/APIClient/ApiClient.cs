using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeCrudNoAuthentication.APIClient
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }
        public async Task<T> GetTAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var studentList  = JsonConvert.DeserializeObject<T>(data);
            return studentList;
        }

        public async Task<T> PostAsync<T>(string uri, object ob)
        {
            
            var response = await _httpClient.PostAsJsonAsync(uri,ob);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
