using System.Net.Http.Headers;

namespace Mpesa.SDK.Helpers
{
    public static class JwtAuthHeader
    {
        public static AuthenticationHeaderValue GetHeader(string accessToken)
        {
            return new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
