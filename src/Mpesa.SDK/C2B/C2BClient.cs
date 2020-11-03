using Mpesa.SDK.LipaNaMpesa;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mpesa.SDK.C2B
{
    /// <summary>
    /// Class to connect to M-Pesa C2B API
    /// </summary>
    public class C2BClient : BaseClient
    {
        public C2BClient(Options options, Func<bool, Task<string>> getAccessToken, Func<HttpClient> httpClientFactory) 
            : base(options, getAccessToken, httpClientFactory)
        {
        }

        /// <summary>
        /// Register to register validation and confirmation URLs on M-Pesa 
        /// </summary>
        /// <param name="responseType">This parameter specifies what is to happen if for any reason the validation URL is nor reachable.</param>
        /// <param name="confirmationUrl">This is the URL that receives the confirmation request from API upon payment completion.  </param>
        /// <param name="validationUrl">This is the URL that receives the validation request from API upon payment submission.</param>
        public async Task<ApiResponse<Response>> RegisterUrl(ResponseTypeEnum responseType, string confirmationUrl, string validationUrl)
        {
            var response = await PostHttp<Response>("/c2b/v1/registerurl", new Dictionary<string, string>
            {
                { "ShortCode", Options.ShortCode },
                { "ResponseType", responseType.ToString() },
                { "ConfirmationURL", confirmationUrl },
                { "ValidationURL", validationUrl }
            });

            return response.ToApiResponse();
        }

        /// <summary>
        /// Simulates a payment made from the client phone's STK/SIM Toolkit menu, and enables you to receive the payment requests in real time. 
        /// </summary>
        /// <param name="transactionType">This is a unique identifier of the transaction type:</param>
        /// <param name="phone"></param>
        /// <param name="amount">This is the amount being transacted</param>
        /// <param name="paymentRef">This is used on CustomerPayBillOnline option only. This is where a customer is expected to enter a unique bill identifier, e.g an Account Number. </param>
        public async Task<ApiResponse<Response>> SimulateTransaction(string phone, string amount, string paymentRef, TransactionTypeEnum transactionType = TransactionTypeEnum.CustomerPayBillOnline)
        {
            if (Options.IsLive)
                throw new InvalidOperationException("Cannot be called on live code.");

            var response = await PostHttp<Response>("c2b/v1/simulate", new Dictionary<string, string>
            {
                { "ShortCode", Options.ShortCode },
                { "CommandId", transactionType.ToString() },
                { "Msisdn", phone },
                { "Amount", amount },
                { "BillRefNumber", paymentRef }
            });

            return response.ToApiResponse();
        }
    }
}
