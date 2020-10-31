using Mpesa.SDK.Account;
using Mpesa.SDK.C2B;
using Mpesa.SDK.LipaNaMpesa;
using System.Threading.Tasks;

namespace Mpesa.SDK.AspNetCore
{
    public interface IC2B
    {
        /// <summary>
        /// Enquires the balance for the specified Short Code
        /// </summary>
        /// <param name="identifierType">Type of orgnanization receiving the transaction</param>
        /// <param name="remarks">Comments that are sent along with the transaction.</param>
        Task<ApiResponse<Response>> QueryBalance(IdentifierTypeEnum identifierType = IdentifierTypeEnum.Organization, string remarks = "Query Account Balance");
        /// <summary>
        /// Check the status of a transaction
        /// </summary>
        /// <param name="transactionId">The trasaction Identifier</param>
        /// <param name="identifierType">Type of orgnanization receiving the transaction</param>
        /// <param name="phone">Used when identifierType is MSISDN</param>
        /// <param name="remarks">Comments that are sent along with the transaction.</param>
        /// <param name="occasion">Occasion</param>
        Task<ApiResponse<Response>> QueryTransactionStatus(string transactionId, IdentifierTypeEnum identifierType, string phone = null, string remarks = "TransactionStatus");
        /// <summary>
        /// Reverses an M-Pesa Transaction
        /// </summary>
        /// <param name="transactionId">Transaction Identifier</param>
        /// <param name="amount">The amount specified for the transaction</param>
        /// <param name="remarks">Comments that are sent along with the transaction.</param>
        /// <param name="occassion">Occasion</param>
        Task<ApiResponse<Response>> RequestReversal(string transactionId, string amount, string remarks = "Reversal");
        /// <summary>
        /// Register to register validation and confirmation URLs on M-Pesa 
        /// </summary>
        /// <param name="responseType">This parameter specifies what is to happen if for any reason the validation URL is nor reachable.</param>
        /// <param name="confirmationUrl">This is the URL that receives the confirmation request from API upon payment completion.  </param>
        /// <param name="validationUrl">This is the URL that receives the validation request from API upon payment submission.</param>
        Task<ApiResponse<Response>> RegisterUrl(ResponseTypeEnum responseType, string confirmationUrl, string validationUrl);
        /// <summary>
        /// Simulates a payment made from the client phone's STK/SIM Toolkit menu, and enables you to receive the payment requests in real time. 
        /// </summary>
        /// <param name="transactionType">This is a unique identifier of the transaction type:</param>
        /// <param name="phone"></param>
        /// <param name="amount">This is the amount being transacted</param>
        /// <param name="paymentRef">This is used on CustomerPayBillOnline option only. This is where a customer is expected to enter a unique bill identifier, e.g an Account Number. </param>
        Task<ApiResponse<Response>> SimulateTransaction(string phone, string amount, string reference, TransactionTypeEnum transactionType = TransactionTypeEnum.CustomerPayBillOnline);
    }
}
