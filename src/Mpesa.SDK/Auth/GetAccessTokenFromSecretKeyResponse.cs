using Newtonsoft.Json;

namespace Mpesa.SDK.Auth
{
    public class GetAccessTokenFromSecretKeyResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
