using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shopping.Api.Options;
using Shopping.Api.Services.Interfaces;

namespace Shopping.Api.Services
{
    public class ApiClient : IApiClient
    {
       
        private readonly string _baseEndpoint;
        private readonly string _token;
        
        public ApiClient(IOptions<ResourceSettings> resourceSettings , IOptions<UserSettings> userSettings)
        {
            _baseEndpoint = resourceSettings?.Value?.BaseUrl  ?? throw new ArgumentNullException($"resourceSettings");
            _token = userSettings?.Value?.Token ?? throw new ArgumentNullException($"userSettings");
        }

        public  async Task<TResponse> GetAsync<TResponse>(string relativePath)
        {
            var result = default(TResponse);
            try
            {
                var endpoint = new Uri(new Uri(_baseEndpoint), relativePath);
                var uriBuilder = new UriBuilder(endpoint) { Query = $"?token={_token}" };
                var requestUrl = uriBuilder.Uri;
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(requestUrl).ConfigureAwait(false); ;
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    result= JsonConvert.DeserializeObject<TResponse>(data);
                }
               
            }

            catch (HttpRequestException ex)
            {
                //log error service call for relativePath and queryString was not sucessful
            }

            catch (Exception ex)
            {
                // log error
            }
            return result;
        }


        public async Task<TResponse> Post<TRequest,TResponse>(TRequest request, string relativePath)
        {
            var result = default(TResponse);
            try
            {
                var endpoint = new Uri(new Uri(_baseEndpoint), relativePath);
                var uriBuilder = new UriBuilder(endpoint) {Query = $"?token={_token}"};
                using (var httpClient = new HttpClient())
                {
                     
                    var response = await httpClient.PostAsync(uriBuilder.Uri, CreateHttpContent<TRequest>(request)).ConfigureAwait(false);
                    var x = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    result= JsonConvert.DeserializeObject<TResponse>(data);
                }
            }
            catch (HttpRequestException ex)
            {
                //log error service call for relativePath and queryString was not sucessful
            }

            catch (Exception ex)
            {
                // log error
            }
            return result;
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }


    }
}
