using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace HttpClientFactoryDemo
{
    public static class HttpPolicies
    {
        /// <summary>
        /// Policy to retry after getting 403 or 5xx error from httpclient
        /// </summary>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() => 
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Forbidden)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3), 
                    (_, waitingTime) => {
                        // Log
                        //Console.WriteLine("Retrying due to polly retry policy");
                    });      

        /// <summary>
        /// Policy to break the retry policy after getting same error for 2 times consecutively
        /// </summary>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError() // Only handle 408 or 5xx errors
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Forbidden) // Configure for additional failures
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(6));

        //public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
        //    HttpPolicyExtensions
        //        .HandleTransientHttpError()
        //        .CircuitBreakerAsync(2, TimeSpan.FromSeconds(7));
    }
}
