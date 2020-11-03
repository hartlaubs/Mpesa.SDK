using System;
using System.Globalization;

namespace Mpesa.SDK.AspNetCore.Callbacks
{
    public class BaseLipaNaMpesa
    {
        public string MerchantRequestId { get; set; }

        public string CheckoutRequestId { get; set; }
    }

    public class LipaNaMpesaError : BaseLipaNaMpesa
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public static LipaNaMpesaError From(StkCallback stkCallback)
        {
            return new LipaNaMpesaError
            {
                MerchantRequestId = stkCallback.MerchantRequestID,
                CheckoutRequestId = stkCallback.CheckoutRequestID,
                ErrorCode = stkCallback.ResultCode,
                ErrorMessage = stkCallback.ResultDesc
            };
        }
    }

    public class LipaNaMpesaResponse : BaseLipaNaMpesa
    {
        public double Amount { get; set; }

        public string ReceiptNumber { get; set; }

        public double? Balance { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

        public string PhoneNumber { get; set; }

        public static LipaNaMpesaResponse From(StkCallback stkCallback)
        {
            var response = new LipaNaMpesaResponse
            {
                MerchantRequestId = stkCallback.MerchantRequestID,
                CheckoutRequestId = stkCallback.CheckoutRequestID
            };

            stkCallback.CallbackMetadata.Item.ForEach(b =>
            {
                if (b.Name == "Amount")
                    response.Amount = (double)b.Value;
                else if (b.Name == "MpesaReceiptNumber")
                    response.ReceiptNumber = (string)b.Value;
                else if (b.Name == "Balance" && b.Value != null)
                    response.Balance = (double)b?.Value;
                else if (b.Name == "TransactionDate")
                    response.TransactionDate = DateTimeOffset.ParseExact(((long)b.Value).ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                else if (b.Name == "PhoneNumber")
                    response.PhoneNumber = ((long)b.Value).ToString();
            });

            return response;
        }
    }
}
