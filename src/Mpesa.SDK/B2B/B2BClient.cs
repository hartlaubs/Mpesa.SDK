using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mpesa.SDK.B2B
{
    /// <summary>
    /// Class to connect to M-Pesa B2C API
    /// </summary>
    public class B2BClient : BaseClient
    {
        public B2BClient(Options options, Func<bool, Task<string>> getAccessToken, Func<HttpClient> httpClientFactory) 
            : base(options, getAccessToken, httpClientFactory)
        {
        }
    }
}
