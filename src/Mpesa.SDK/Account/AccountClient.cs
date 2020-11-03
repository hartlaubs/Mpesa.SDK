using Damurka.Generator;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mpesa.SDK.Account
{
    /// <summary>
    /// Contains the API calls related to ShortCode Account
    /// i.e. Account Balance, Transaction Status, Transaction Reversal
    /// </summary>
    public class AccountClient : BaseClient
    {
        public AccountClient(Options options, Func<bool, Task<string>> getAccessToken, Func<HttpClient> httpClientFactory) 
            : base(options, getAccessToken, httpClientFactory)
        {
        }

        /// <summary>
        /// Enquires the balance for the specified Short Code
        /// </summary>
        /// <param name="identifierType">Type of orgnanization receiving the transaction</param>
        /// <param name="remarks">Comments that are sent along with the transaction.</param>
        public async Task<ApiResponse<Response>> QueryBalance(IdentifierTypeEnum identifierType = IdentifierTypeEnum.Organization, string remarks = "Query Account Balance")
        {
            if (identifierType == IdentifierTypeEnum.MSISDN)
                throw new Exception("SDK does not support MSISDN");

            var requestId = ShortId.Generate(32);

            var response = await PostHttp<Response>("/accountbalance/v1/query", new Dictionary<string, string>
            {
                { "Initiator", Options.Initiator },
                { "SecurityCredential", Options.SecurityCredential },
                { "CommandID", "AccountBalance" },
                { "PartyA", Options.ShortCode },
                { "IdentifierType", identifierType.ToString("D") },
                { "Remarks", remarks },
                { "QueueTimeOutURL", $"{Options.GetQueueTimeoutURL(requestId)}/balance" },
                { "ResultURL", $"{Options.GetResultRL(requestId)}/balance" },
            });

            var res = response.ToApiResponse();
            if (res.Success) res.Data.RequestId = requestId;
            return res;
        }

        /// <summary>
        /// Check the status of a transaction
        /// </summary>
        /// <param name="transactionId">The trasaction Identifier</param>
        /// <param name="identifierType">Type of orgnanization receiving the transaction</param>
        /// <param name="phone">Used when identifierType is MSISDN</param>
        /// <param name="remarks">Comments that are sent along with the transaction.</param>
        /// <param name="occasion">Occasion</param>
        public async Task<ApiResponse<Response>> QueryTransactionStatus(string transactionId,  IdentifierTypeEnum identifierType, string phone = null, string remarks = "TransactionStatus", string occasion = "TransactionStaus")
        {
            if (IdentifierTypeEnum.MSISDN == identifierType && string.IsNullOrEmpty(phone))
                throw new ArgumentNullException("Phone cannot be null", nameof(phone));

            var requestId = ShortId.Generate(32);

            var response = await PostHttp<Response>("/transactionstatus/v1/query", new Dictionary<string, string>
            {
                { "Initiator", Options.Initiator },
                { "SecurityCredential", Options.SecurityCredential },
                { "CommandID", "TransactionStatusQuery" },
                { "TransactionID", transactionId },
                { "PartyA", identifierType == IdentifierTypeEnum.MSISDN ? phone : Options.ShortCode },
                { "IdentifierType", identifierType.ToString("D") },
                { "QueueTimeOutURL", $"{ Options.GetQueueTimeoutURL(requestId)}/status" },
                { "ResultURL", $"{ Options.GetResultRL(requestId)}/status" },
                { "Remarks", remarks },
                { "Occasion", occasion }
            });

            var res = response.ToApiResponse();
            if (res.Success) res.Data.RequestId = requestId;
            return res;
        }

        /// <summary>
        /// Reverses an M-Pesa Transaction
        /// </summary>
        /// <param name="transactionId">Transaction Identifier</param>
        /// <param name="amount">The amount specified for the transaction</param>
        /// <param name="remarks">Comments that are sent along with the transaction.</param>
        /// <param name="occassion">Occasion</param>
        public async Task<ApiResponse<Response>> RequestReversal(string transactionId, string amount, string remarks = "Reversal", string occassion = "Reversal")
        {
            var requestId = ShortId.Generate(32);
            var response = await PostHttp<Response>("/reversal/v1/request", new Dictionary<string, string>
            {
                { "Initiator", Options.Initiator },
                { "SecurityCredential", Options.SecurityCredential },
                { "CommandID", "TransactionReversal" },
                { "TransactionID", transactionId },
                { "Amount", amount },
                { "ReceiverParty", Options.ShortCode },
                { "RecieverIdentifierType", "11" },
                { "Remarks", remarks },
                { "Occasion", occassion },
                { "QueueTimeOutURL", $"{ Options.GetQueueTimeoutURL(requestId)}/reversal" },
                { "ResultURL", $"{ Options.GetResultRL(requestId)}/reversal" },
            });

            var res = response.ToApiResponse();
            if (res.Success) res.Data.RequestId = requestId;
            return res;
        }
    }
}
