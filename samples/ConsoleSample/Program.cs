using Mpesa.SDK.Account;
using System;
using System.Threading.Tasks;

namespace Mpesa.SDK.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var options = new MpesaApiOptions
            {
                ShortCode = "",
                Initiator = "",
                InitiatorPassword = "",
                PassKey = "",
                IsLive = true,
                QueueTimeOutURL = "",
                ResultURL = ""
            };

            var api = new MpesaApi("", "", options);
            var balance = await api.Account.QueryBalance();
            if (!balance.Success)
                Console.WriteLine(balance.Error.ErrorMessage);

            var transaction = await api.Account.QueryTransactionStatus("OJ68HHO4H", IdentifierTypeEnum.Organization);
            if (!transaction.Success)
                Console.WriteLine(transaction.Error.ErrorMessage);

            var reverse = await api.Account.RequestReversal("OIT8YS6I8Y", "850", "Accounting error");
            if (!reverse.Success)
                Console.WriteLine(reverse.Error.ErrorMessage);

            var stkPush = await api.LipaNaMpesa.PushStk("254717774421", "100", "Home", "Home repairs");
            if (!stkPush.Success)
                Console.WriteLine(stkPush.Error.ErrorMessage);

            var stkQuery = await api.LipaNaMpesa.QueryStatus(/*"ws_CO_30102020004040298972"*/ stkPush.Data.CheckoutRequestID);
            if (!stkQuery.Success)
                Console.WriteLine(stkQuery.Error.ErrorMessage);

            var b2c = await api.B2CClient.SendMoney("254717774421", "1000");
            if (!b2c.Success)
                Console.WriteLine(b2c.Error.ErrorMessage);
        }
    }
}
