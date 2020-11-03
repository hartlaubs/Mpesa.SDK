using Mpesa.SDK.AspNetCore.Extensions;
using System.Collections.Generic;

namespace Mpesa.SDK.AspNetCore.Callbacks
{
    public class BalanceQueryResponse : BaseResponse
    {
        public Balance WorkingAccount { get; set; }

        public Balance UtilityAccount { get; set; }

        public Balance ChargesPaidAccount { get; set; }

        public Balance OrganizationSettlementAccount { get; set; }

        public static BalanceQueryResponse From(Result result)
        {
            var callback = new BalanceQueryResponse
            {
                OriginatorConversationID = result.OriginatorConversationID,
                ConversationID = result.ConversationID,
                TransactionID = result.TransactionID
            };

            if (result.ResultParameters.ResultParameter is Newtonsoft.Json.Linq.JArray array)
            {
                var list = array.ToObject<List<KeyValuePair>>();
                list.ForEach(p =>
                {
                    if (p.Key == "AccountBalance")
                    {
                        var accounts = ((string)p.Value).Split("&");
                        foreach (var account in accounts)
                        {
                            var balances = account.Split("|");
                            if (balances[0] == "Working Account")
                                callback.WorkingAccount = balances.GetBalance();
                            else if (balances[0] == "Utility Account")
                                callback.UtilityAccount = balances.GetBalance();
                            else if (balances[0] == "Charges Paid Account")
                                callback.ChargesPaidAccount = balances.GetBalance();
                            else if (balances[0] == "Organization Settlement Account")
                                callback.OrganizationSettlementAccount = balances.GetBalance();
                        }
                    }
                });
            }

            return callback;
        }
    }

    public class Balance
    {
        public double Current { get; set; }
        public double Available { get; set; }
        public double Reserved { get; set; }
        public double Unclear { get; set; }
    }
}
