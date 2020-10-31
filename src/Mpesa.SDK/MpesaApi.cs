using Mpesa.SDK.Account;
using Mpesa.SDK.Auth;
using Mpesa.SDK.B2B;
using Mpesa.SDK.B2C;
using Mpesa.SDK.C2B;
using Mpesa.SDK.LipaNaMpesa;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mpesa.SDK
{
    public class MpesaApi
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly Func<HttpClient> _httpClientFactory;

        private readonly Options _options;
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private string _accessToken;

        private AuthClient _auth;
        private AccountClient _account;
        private LipaNaMpesaClient _lipaNaMpesa;
        private B2BClient _b2bClient;
        private B2CClient _b2cClient;
        private C2BClient _c2bClient;

        public AuthClient Auth => _auth ??= new AuthClient(_consumerKey, _consumerSecret, _options.BaseUri);
        public AccountClient Account => _account ??= new AccountClient(_options, GetAccessToken, HttpClientFactoryOrStaticInstance());
        public LipaNaMpesaClient LipaNaMpesa => _lipaNaMpesa ??= new LipaNaMpesaClient(_options, GetAccessToken, HttpClientFactoryOrStaticInstance());
        public B2BClient B2BClient => _b2bClient ??= new B2BClient(_options, GetAccessToken, HttpClientFactoryOrStaticInstance());
        public B2CClient B2CClient => _b2cClient ??= new B2CClient(_options, GetAccessToken, HttpClientFactoryOrStaticInstance());
        public C2BClient C2BClient => _c2bClient ??= new C2BClient(_options, GetAccessToken, HttpClientFactoryOrStaticInstance());


        public MpesaApi(string consumerKey, string consumerSecret, MpesaApiOptions options, Func<HttpClient> httpClientFactory = null)
        {
            _httpClientFactory = httpClientFactory;

            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _options = Options.From(options);
        }

        /// <summary>
        /// THis method fetches and caches an Access Token, requested from the Auth API.
        /// </summary>
        /// <param name="renew"></param>
        /// <returns></returns>
        protected async Task<string> GetAccessToken(bool renew = false)
        {
            if (_accessToken == null || renew)
            {
                var result = await Auth.GetAccessToken();
               _accessToken = result.AccessToken;
            }

            return _accessToken;
        }

        /// <summary>
        /// Returns either the iven HttpClientFactory or a default implementation.
        /// </summary>
        /// <returns></returns>
        protected Func<HttpClient> HttpClientFactoryOrStaticInstance()
        {
            return _httpClientFactory ?? (() => _httpClient);
        }
    }
}
