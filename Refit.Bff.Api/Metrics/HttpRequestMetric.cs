using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Refit.Bff.Api.Metrics
{
    public static class HttpRequestMetric
    {
        public static async Task<TResult> RunHttpAsync<TResult>(Func<Task<HttpResponseMessage>> callHttpRequest, ILogger logger)
        {
            logger.LogInformation("Request started..." + DateTime.Now.ToString());
            var response = await callHttpRequest().ConfigureAwait(false);
            logger.LogInformation("Request finished..." + DateTime.Now.ToString());
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TResult>(content,
                                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }

            throw new HttpRequestException();
        }

        public static async Task<TResult> RunFuncAsync<TResult>(Func<Task<TResult>> callFunc, ILogger logger)
        {
            logger.LogInformation("Func started..." + DateTime.Now.ToString());
            var response = await callFunc().ConfigureAwait(false);
            logger.LogInformation("Func finished..." + DateTime.Now.ToString());

            return response;
        }
    }
}