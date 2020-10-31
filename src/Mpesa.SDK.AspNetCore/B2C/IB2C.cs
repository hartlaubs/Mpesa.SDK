using Mpesa.SDK.Account;
using Mpesa.SDK.B2C;
using System.Threading.Tasks;

namespace Mpesa.SDK.AspNetCore
{
    public interface IB2C
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
        Task<ApiResponse<Response>> QueryTransactionStatus(string transactionId, IdentifierTypeEnum identifierType, string phone = null, string remarks = "TransactionStatus");
        /// <summary>
        /// Reverses an M-Pesa Transaction
        /// </summary>
        /// <param name="transactionId">Transaction Identifier</param>
        /// <param name="amount">The amount specified for the transaction</param>
        /// <param name="remarks">Comments that are sent along with the transaction.</param>
        Task<ApiResponse<Response>> RequestReversal(string transactionId, string amount, string remarks = "Reversal");
        /// <summary>
        /// Send Money from Business to Customer
        /// </summary>
        /// <param name="phone">Phone to Send money</param>
        /// <param name="amount">Amonunt</param>
        /// <param name="commandId">The type of transaction being perfomed</param>
        /// <param name="comment">Comments that are sent along with the transaction.</param>
        Task<ApiResponse<Response>> SendMoney(string phone, string amount, B2CCommandIdEnum commandId = B2CCommandIdEnum.SalaryPayment, string remarks = "B2C Payment");
    }
}
