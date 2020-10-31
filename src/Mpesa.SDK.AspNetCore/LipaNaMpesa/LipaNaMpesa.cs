using Microsoft.Extensions.Options;
using Mpesa.SDK.LipaNaMpesa;
using System;
using System.Threading.Tasks;

namespace Mpesa.SDK.AspNetCore
{

    public class LipaNaMpesa : ILipaNaMpesa
    {
        private readonly MpesaApi api;

        public LipaNaMpesa(IOptions<LipaNaMpesaOptions> options)
        {
            var opt = options.Value;
            api = new MpesaApi(opt.ConsumerKey, opt.ConsumerSecret, opt.ToMpesaOptions());
        }

        public async Task<ApiResponse<PushStkResponse>> PushStk(string phone, string amount, string account, string description = "Lipa na Mpesa Online", TransactionTypeEnum transactionType = TransactionTypeEnum.CustomerPayBillOnline) =>
            await api.LipaNaMpesa.PushStk(phone, amount, account, description, transactionType);

        public async Task<ApiResponse<QueryStkResponse>> QueryStatus(string checkoutRequestId) =>
            await api.LipaNaMpesa.QueryStatus(checkoutRequestId);
    }
}
