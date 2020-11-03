using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mpesa.SDK.AspNetCore.Callbacks
{
    public class StatusQueryResponse : BaseResponse
    {
        public string DebitPartyName { get; set; }

        public string CreditPartyName { get; set; }

        // public string OriginatorConversationID { get; set; }

        public DateTimeOffset InitiatedTime { get; set; }

        public string DebitAccountType { get; set; }

        public string DebitPartyCharges { get; set; }

        public string TransactionReason { get; set; }

        public string TransactionStatus { get; set; }

        public DateTimeOffset FinalisedTime { get; set; }

        public double Amount { get; set; }

        // public string ConversationID { get; set; }

        public string ReceiptNo { get; set; }

        public static StatusQueryResponse From(Result result)
        {
            var response = new StatusQueryResponse
            {
                OriginatorConversationID = result.OriginatorConversationID,
                ConversationID = result.ConversationID,
                TransactionID = result.TransactionID,
            };

            if (result.ResultParameters.ResultParameter is Newtonsoft.Json.Linq.JArray array)
            {
                var list = array.ToObject<List<KeyValuePair>>();
                list.ForEach(p =>
                {
                    if (p.Key == "DebitPartyName")
                        response.DebitPartyName = (string)p.Value;
                    else if (p.Key == "CreditPartyName")
                        response.CreditPartyName = (string)p.Value;
                    else if (p.Key == "InitiatedTime")
                        response.InitiatedTime = DateTimeOffset.ParseExact(((long)p.Value).ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                    else if (p.Key == "DebitAccountType")
                        response.DebitAccountType = (string)p.Value;
                    else if (p.Key == "DebitPartyCharges")
                        response.DebitPartyCharges = (string)p.Value;
                    else if (p.Key == "TransactionReason")
                        response.TransactionReason = (string)p.Value;
                    else if (p.Key == "TransactionStatus")
                        response.TransactionStatus = (string)p.Value;
                    else if (p.Key == "FinalisedTime")
                        response.FinalisedTime = DateTimeOffset.ParseExact(((long)p.Value).ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                    else if (p.Key == "Amount")
                        response.Amount = (double)p.Value;
                    else if (p.Key == "ReceiptNo")
                        response.ReceiptNo = (string)p.Value;
                });
            }

            return response;
        }
    }
}
