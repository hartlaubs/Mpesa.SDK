using Mpesa.SDK.LipaNaMpesa;
using System.Threading.Tasks;

namespace Mpesa.SDK.AspNetCore
{
    public interface ILipaNaMpesa
    {
        /// <summary>
        /// Initiate an online transaction on behalf of the customer
        /// </summary>
        /// <param name="phone">The phone number sending money. </param>
        /// <param name="amount">The amount to be transacted</param>
        /// <param name="account">The account reference</param>
        /// <param name="description">This is any additional information/comment that can be sent along with the request from your system.</param>
        /// <param name="transactionType">This is the transaction type that is used to identify the transaction when sending the request to M-Pesa</param>
        Task<ApiResponse<PushStkResponse>> PushStk(string phone, string amount, string account, string description = "Lipa na Mpesa Online", TransactionTypeEnum transactionType = TransactionTypeEnum.CustomerPayBillOnline);
        /// <summary>
        /// Check the status of a Lipa Na M-Pesa Online Payment.
        /// </summary>
        /// <param name="checkoutRequestId">his is a global unique identifier of the processed checkout transaction request.</param>
        Task<ApiResponse<QueryStkResponse>> QueryStatus(string checkoutRequestId);
    }
}
