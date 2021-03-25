using Microsoft.Extensions.DependencyInjection;

namespace HttpClientFactoryDemo.Extensions
{
    public static class IHttpClientBuilderExtensions
    {
        /// <summary>
        /// Simply adds all the http policies to HttpClient instance
        /// </summary>
        /// <param name="httpClientBuilder"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddPolicyHandlers(this IHttpClientBuilder httpClientBuilder)
        {
            return httpClientBuilder
                .AddPolicyHandler(HttpPolicies.GetRetryPolicy())
                .AddPolicyHandler(HttpPolicies.GetCircuitBreakerPolicy());
        }
    }
}
