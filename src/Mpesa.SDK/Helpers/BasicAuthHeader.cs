using System;
using System.Net.Http.Headers;
using System.Text;

namespace Mpesa.SDK.Helpers
{
    public static class BasicAuthHeader
    {
        public static AuthenticationHeaderValue GetHeader(string key, string secret)
        {
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{key}:{secret}")));
        }
    }
}
