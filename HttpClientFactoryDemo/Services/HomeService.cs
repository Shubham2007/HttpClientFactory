using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryDemo.Services
{
    public class HomeService : IHomeService
    {
        // Managed HttpClient instance from HttpClientFactory from DI
        private readonly HttpClient _httpClient;

        public HomeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Return Posts data as json
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetPosts()
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync("/posts");

            if(httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsStringAsync();
            }

            throw new Exception("Cannot fetch posts...");
        }
    }
}
