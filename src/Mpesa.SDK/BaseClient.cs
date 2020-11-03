using Mpesa.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mpesa.SDK
{
    public class BaseClient
    {
        protected readonly Options Options;
        private readonly Func<bool, Task<string>> _getAccessToken;
        private readonly Func<HttpClient> _httpClientFactory;

        public BaseClient(Options options, Func<bool, Task<string>> getAccessToken, Func<HttpClient> httpClientFactory)
        {
            Options = options;
            _getAccessToken = getAccessToken;
            _httpClientFactory = httpClientFactory;
        }

        protected async Task<QuickResponse<T>> PostHttp<T>(string url, Dictionary<string, string> parameters)
        {
            return await SendHttp<T>(() => new HttpRequestMessage(HttpMethod.Post, Options.BaseUri + url)
            {
                Content = HttpClientHelpers.GetPostBody(parameters)
            });
        }

        private async Task<QuickResponse<T>> SendHttp<T>(Func<HttpRequestMessage> requestFunc)
        {
            try
            {
                var request = requestFunc();
                await SetAuthHeader(request, false);

                var response = await _httpClientFactory().SendAsync(request);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    request = requestFunc();
                    await SetAuthHeader(request, true);
                    response = await _httpClientFactory().SendAsync(request);
                }

                return await QuickResponse<T>.FromMessage(response);
            }
            catch (Exception ex)
            {
                return new QuickResponse<T>
                {
                    Error = new ApiError { ErrorMessage = GetRecursiveErrorMessage(ex) }
                };
            }
        }

        private async Task SetAuthHeader(HttpRequestMessage message, bool renew)
        {
            var token = await _getAccessToken(renew);
            message.Headers.Authorization = JwtAuthHeader.GetHeader(token);
        }

        private string GetRecursiveErrorMessage(Exception ex, string delimiter = " --- ")
        {
            var sb = new StringBuilder();
            var currentException = ex;
            while (currentException != null)
            {
                if (!string.IsNullOrEmpty(sb.ToString()))
                    sb.Append(delimiter);
                sb.Append(currentException.Message);

                currentException = currentException.InnerException;
            }

            return sb.ToString();
        }
    }
}
