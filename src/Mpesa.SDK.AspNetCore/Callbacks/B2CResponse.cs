using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mpesa.SDK.AspNetCore.Callbacks
{
    public class B2CResponse : BaseResponse
    {
        public double TransactionAmount { get; set; }

        public string TransactionReceipt { get; set; }

        public string ReceiverPartyPublicName { get; set; }

        public DateTimeOffset TransactionCompletedDateTime { get; set; }

        public double B2CUtilityAccountAvailableFunds { get; set; }

        public double B2CWorkingAccountAvailableFunds { get; set; }

        public string B2CRecipientIsRegisteredCustomer { get; set; }

        public double B2CChargesPainAccountAvailableFunds { get; set; }

        public static B2CResponse From(Result result)
        {
            var b2cResponse = new B2CResponse
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
                    if (p.Key == "TransactionAmount")
                        b2cResponse.TransactionAmount = (double)p.Value;
                    else if (p.Key == "TransactionReceipt")
                        b2cResponse.TransactionReceipt = (string)p.Value;
                    else if (p.Key == "ReceiverPartyPublicName")
                        b2cResponse.ReceiverPartyPublicName = (string)p.Value;
                    else if (p.Key == "TransactionCompletedDateTime")
                        b2cResponse.TransactionCompletedDateTime = DateTimeOffset.ParseExact((string)p.Value, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    else if (p.Key == "B2CUtilityAccountAvailableFunds")
                        b2cResponse.B2CUtilityAccountAvailableFunds = (double)p.Value;
                    else if (p.Key == "B2CWorkingAccountAvailableFunds")
                        b2cResponse.B2CWorkingAccountAvailableFunds = (double)p.Value;
                    else if (p.Key == "B2CRecipientIsRegisteredCustomer")
                        b2cResponse.B2CRecipientIsRegisteredCustomer = (string)p.Value;
                    else if (p.Key == "B2CChargesPainAccountAvailableFunds")
                        b2cResponse.B2CChargesPainAccountAvailableFunds = (double)p.Value;
                });
            }

            return b2cResponse;
        }

    }
}
