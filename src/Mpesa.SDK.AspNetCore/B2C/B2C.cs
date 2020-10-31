using Microsoft.Extensions.Options;
using Mpesa.SDK.Account;
using Mpesa.SDK.B2C;
using System.Threading.Tasks;

namespace Mpesa.SDK.AspNetCore
{
    public class B2C : IB2C
    {
        private readonly MpesaApi api;

        public B2C(IOptions<B2COptions> options)
        {
            var opt = options.Value;
            api = new MpesaApi(opt.ConsumerKey, opt.ConsumerSecret, opt.ToMpesaOptions());
        }

        public Task<ApiResponse<Response>> QueryBalance(IdentifierTypeEnum identifierType = IdentifierTypeEnum.Organization, string remarks ="Query Account Balance") =>
            api.Account.QueryBalance(identifierType, remarks);

        public Task<ApiResponse<Response>> QueryTransactionStatus(string transactionId, IdentifierTypeEnum identifierType, string phone = null, string remarks = "TransactionStatus") =>
            api.Account.QueryTransactionStatus(transactionId, identifierType, phone, remarks);

        public Task<ApiResponse<Response>> RequestReversal(string transactionId, string amount, string remarks = "Reversal") =>
            api.Account.RequestReversal(transactionId, amount, remarks);

        public Task<ApiResponse<Response>> SendMoney(string phone, string amount, B2CCommandIdEnum commandId = B2CCommandIdEnum.SalaryPayment, string remarks = "B2C Payment") =>
            api.B2CClient.SendMoney(phone, amount, commandId, remarks);
    }
}
