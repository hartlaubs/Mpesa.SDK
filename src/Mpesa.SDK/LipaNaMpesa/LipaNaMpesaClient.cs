using Damurka.Generator;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mpesa.SDK.LipaNaMpesa
{
    public class LipaNaMpesaClient : BaseClient
    {
        public LipaNaMpesaClient(Options options, Func<bool, Task<string>> getAccessToken, Func<HttpClient> httpClientFactory) 
            : base(options, getAccessToken, httpClientFactory)
        {
        }

        /// <summary>
        /// Check the status of a Lipa Na M-Pesa Online Payment.
        /// </summary>
        /// <param name="checkoutRequestId">his is a global unique identifier of the processed checkout transaction request.</param>
        public async Task<ApiResponse<QueryStkResponse>> QueryStatus(string checkoutRequestId)
        {
            var response = await PostHttp<QueryStkResponse>("/stkpushquery/v1/query", new Dictionary<string, string>
            {
                { "BusinessShortCode", Options.ShortCode },
                { "Password", Options.EncodedPassword },
                { "Timestamp", Options.Timestamp },
                { "CheckoutRequestID", checkoutRequestId }
            });

            return response.ToApiResponse();
        }

        /// <summary>
        /// Initiate an online transaction on behalf of the customer
        /// </summary>
        /// <param name="phone">The phone number sending money. </param>
        /// <param name="amount">The amount to be transacted</param>
        /// <param name="account">The account reference</param>
        /// <param name="description">This is any additional information/comment that can be sent along with the request from your system.</param>
        /// <param name="transactionType">This is the transaction type that is used to identify the transaction when sending the request to M-Pesa</param>
        public async Task<ApiResponse<PushStkResponse>> PushStk(string phone, string amount, string account, string description = "Lipa na Mpesa Online", TransactionTypeEnum transactionType = TransactionTypeEnum.CustomerPayBillOnline)
        {
            var requestId = ShortId.Generate(32);
            var response = await PostHttp<PushStkResponse>("/stkpush/v1/processrequest", new Dictionary<string, string>
            {
                { "BusinessShortCode", Options.ShortCode },
                { "Password", Options.EncodedPassword },
                { "Timestamp", Options.Timestamp },
                { "TransactionType", transactionType.ToString() },
                { "Amount", amount },
                { "PartyA", phone },
                { "PartyB", Options.ShortCode },
                { "PhoneNumber", phone },
                { "CallBackURL", $"{Options.GetResultRL(requestId)}/lnm" },
                { "AccountReference", account },
                { "TransactionDesc", description }
            });

            var res = response.ToApiResponse();
            if (res.Success) res.Data.RequestId = requestId;
            return res;
        }
    }
}
