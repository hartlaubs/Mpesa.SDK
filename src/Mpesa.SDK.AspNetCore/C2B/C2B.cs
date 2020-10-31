using Microsoft.Extensions.Options;
using Mpesa.SDK.Account;
using Mpesa.SDK.C2B;
using Mpesa.SDK.LipaNaMpesa;
using System.Threading.Tasks;

namespace Mpesa.SDK.AspNetCore
{
    public class C2B : IC2B
    {
        private readonly MpesaApi api;

        public C2B(IOptions<C2BOptions> options)
        {
            var opt = options.Value;
            api = new MpesaApi(opt.ConsumerKey, opt.ConsumerSecret, opt.ToMpesaOptions());
        }

        public Task<ApiResponse<Response>> QueryBalance(IdentifierTypeEnum identifierType = IdentifierTypeEnum.Organization, string remarks = "Query Account Balance") =>
            api.Account.QueryBalance(identifierType, remarks);

        public Task<ApiResponse<Response>> QueryTransactionStatus(string transactionId, IdentifierTypeEnum identifierType, string phone = null, string remarks = "TransactionStatus") =>
            api.Account.QueryTransactionStatus(transactionId, identifierType, phone, remarks);

        public Task<ApiResponse<Response>> RequestReversal(string transactionId, string amount, string remarks = "Reversal") =>
            api.Account.RequestReversal(transactionId, amount, remarks);

        public Task<ApiResponse<Response>> RegisterUrl(ResponseTypeEnum responseType, string confirmationUrl, string validationUrl) =>
            api.C2BClient.RegisterUrl(responseType, confirmationUrl, validationUrl);

        public Task<ApiResponse<Response>> SimulateTransaction(string phone, string amount, string reference, TransactionTypeEnum transactionType = TransactionTypeEnum.CustomerPayBillOnline) =>
            api.C2BClient.SimulateTransaction(phone, amount, reference, transactionType);
    }
}
