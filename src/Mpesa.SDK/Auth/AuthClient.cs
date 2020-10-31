using Mpesa.SDK.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mpesa.SDK.Auth
{
    public class AuthClient
    {
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private readonly string _authUri;

        public AuthClient(string consumerKey, string consumerSecret, string authUri)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _authUri = authUri;
        }

        public async Task<GetAccessTokenFromSecretKeyResponse> GetAccessToken()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_authUri)
            };
            client.DefaultRequestHeaders.Authorization = BasicAuthHeader.GetHeader(_consumerKey, _consumerSecret);

            var clientResponse = await client.GetAsync("/oauth/v1/generate?grant_type=client_credentials");
            var response = await QuickResponse<GetAccessTokenFromSecretKeyResponse>.FromMessage(clientResponse);

            if (response.Data?.AccessToken == null)
                throw new Exception($"Could not get access token. Error: {response.ResponseBody}");

            return response.Data;
        }
    }
}
