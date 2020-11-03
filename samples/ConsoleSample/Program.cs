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
                ShortCode = "600111",
                Initiator = "testapi111",
                InitiatorPassword = "Safaricom111!",
                PassKey = "bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919",
                IsLive = true,
                QueueTimeoutURL = "https://41ed377c8e62.ngrok.io/webhoo",
                ResultURL = "https://41ed377c8e62.ngrok.io/result"
            };

            var api = new MpesaApi("W77IT5mNOnBwkUwWhNxAKPS61uZur3pj", "qGkYG0ZUkfXFIRZE", options);

            var balance = await api.Account.QueryBalance();
            if (!balance.Success)
                Console.WriteLine(balance.Error.ErrorMessage);

            var transaction = await api.Account.QueryTransactionStatus("OJ68HHO4H", IdentifierTypeEnum.Organization);
            if (!transaction.Success)
                Console.WriteLine(transaction.Error.ErrorMessage);

            var reverse = await api.Account.RequestReversal("OIT8YS6I8Y", "850", "Accounting error");
            if (!reverse.Success)
                Console.WriteLine(reverse.Error.ErrorMessage);

            var stkPush = await api.LipaNaMpesa.PushStk("254722000000", "100", "Home", "Home repairs");
            if (!stkPush.Success)
                Console.WriteLine(stkPush.Error.ErrorMessage);

            var stkQuery = await api.LipaNaMpesa.QueryStatus("ws_CO_30102020004040278972");
            if (!stkQuery.Success)
                Console.WriteLine(stkQuery.Error.ErrorMessage);

            var b2c = await api.B2CClient.SendMoney("254722000000", "1000");
            if (!b2c.Success)
                Console.WriteLine(b2c.Error.ErrorMessage);
        }
    }
}
