using System.Collections.Generic;

namespace Mpesa.SDK.AspNetCore.Callbacks
{
    public class NameValuePair
    {
        public string Name { get; set; }

        public object Value { get; set; }
    }

    public class CallbackMetadata
    {
        public List<NameValuePair> Item { get; set; }
    }

    public class StkCallback
    {
        public string MerchantRequestID { get; set; }

        public string CheckoutRequestID { get; set; }

        public int ResultCode { get; set; }

        public string ResultDesc { get; set; }

        public CallbackMetadata CallbackMetadata { get; set; }
    }

    public class Body
    {
        public StkCallback stkCallback { get; set; }
    }

    public class BodyResponse
    {
        public Body Body { get; set; }
    }
}
