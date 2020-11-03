using Damurka.Generator;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mpesa.SDK.B2C
{
    /// <summary>
    /// Class to connect to M-Pesa B2C API
    /// </summary>
    public class B2CClient : BaseClient
    {
        public B2CClient(Options options, Func<bool, Task<string>> getAccessToken, Func<HttpClient> httpClientFactory) 
            : base(options, getAccessToken, httpClientFactory)
        {
        }

        /// <summary>
        /// Send Money from Business to Customer
        /// </summary>
        /// <param name="phone">Phone to Send money</param>
        /// <param name="amount">Amonunt</param>
        /// <param name="commandId">The type of transaction being perfomed</param>
        /// <param name="comment">Comments that are sent along with the transaction.</param>
        /// <param name="occasion">Occasion</param>
        public async Task<ApiResponse<Response>> SendMoney(string phone, string amount, B2CCommandIdEnum commandId = B2CCommandIdEnum.SalaryPayment, string comment = "B2C Payment", string occasion = "B2C Payment")
        {
            var requestId = ShortId.Generate(32);
            var response = await PostHttp<Response>("/b2c/v1/paymentrequest", new Dictionary<string, string>
            {
                { "InitiatorName", Options.Initiator },
                { "SecurityCredential", Options.SecurityCredential },
                { "CommandID", commandId.ToString() },
                { "Amount", amount },
                { "PartyA", Options.ShortCode },
                { "PartyB", phone },
                { "Remarks", comment },
                { "Occassion", occasion },
                { "QueueTimeOutURL", $"{Options.GetQueueTimeoutURL(requestId)}/b2c" },
                { "ResultURL", $"{Options.GetResultRL(requestId)}/b2c" },
            });

            var res = response.ToApiResponse();
            if (res.Success) res.Data.RequestId = requestId;
            return res;
        }
    }
}
