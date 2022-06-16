using ReckonAPI.Service.Interfaces;
using ReckonAPI.Shared.Entities;
using ReckonAPI.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ReckonAPI.Service.Services
{
    public class ReckonTextService : IReckonTextService
    {
        private readonly HttpClient _httpClient;

        public ReckonTextService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<string>> GetSubTextsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, ConfigHelper.SubTextUrl);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseValue = await response.Content?.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SubText>(responseValue);

            return result.SubTexts;
        }

        public async Task<string> GetTextToSearchAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, ConfigHelper.TextToSearchUrl);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseValue = await response.Content?.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TextToSearch>(responseValue);
            return result.Text;
        }

        public async Task PostResultAsync(WordCountOutput postResult)
        {            
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var content = JsonConvert.SerializeObject(postResult, new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            });
            
            var request = new HttpRequestMessage(HttpMethod.Post, ConfigHelper.PostResultUrl);
            
            request.Content = new StringContent(content);
            
            var response = await _httpClient.SendAsync(request);
            
            response.EnsureSuccessStatusCode();
        }
    }
}
