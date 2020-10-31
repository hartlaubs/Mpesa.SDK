using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mpesa.SDK.Helpers
{
    public class HttpClientHelpers
    {
        public static HttpContent GetPostBody(Dictionary<string, string> parameters)
        {
            return new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
        }

        public static HttpContent GetJsonBody(object value)
        {
            return new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
        }
    }

    public class QuickResponse
    {
        public HttpResponseMessage Message { get; set; }

        public string ResponseBody { get; set; }

        public ApiError Error { get; set; }


        public ApiResponse ToApiResponse()
        {
            return new ApiResponse { Error = Error };
        }

        public static async Task<QuickResponse> FromMessage(HttpResponseMessage message)
        {
            var response = new QuickResponse
            {
                Message = message,
                ResponseBody = await message.Content.ReadAsStringAsync()
            };

            if (!message.IsSuccessStatusCode)
                response.HandleFailedCall();

            return response;
        }

        protected void HandleFailedCall()
        {
            try
            {
                Error = JsonConvert.DeserializeObject<ApiError>(ResponseBody);
                if (Error == null)
                {
                    Error = new ApiError
                    {
                        ErrorMessage = !string.IsNullOrEmpty(ResponseBody) ? ResponseBody : Message.StatusCode.ToString()
                    };
                }
            }
            catch (Exception)
            {
                Error = new ApiError
                {
                    ErrorMessage = !string.IsNullOrEmpty(ResponseBody) ? ResponseBody : Message.StatusCode.ToString()
                };
            }
        }
    }

    public class QuickResponse<T> : QuickResponse
    {
        public T Data { get; set; }

        public new ApiResponse<T> ToApiResponse()
        {
            return new ApiResponse<T> 
            {
                Error = Error,
                Data = Data
            };
        }

        public new static async Task<QuickResponse<T>> FromMessage(HttpResponseMessage message)
        {
            var response = new QuickResponse<T>
            {
                Message = message,
                ResponseBody = await message.Content.ReadAsStringAsync()
            };

            if (message.IsSuccessStatusCode)
                response.Data = JsonConvert.DeserializeObject<T>(response.ResponseBody);
            else
                response.HandleFailedCall();

            return response;
        }
    }
}
