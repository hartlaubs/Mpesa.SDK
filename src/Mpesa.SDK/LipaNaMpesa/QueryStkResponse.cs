using Newtonsoft.Json;

namespace Mpesa.SDK.LipaNaMpesa
{
    public class QueryStkResponse
    {
        [JsonProperty("ResponseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("ResponseDescription")]
        public string ResponseDescription { get; set; }

        [JsonProperty("MerchantRequestID")]
        public string MerchantRequestID { get; set; }

        [JsonProperty("CheckoutRequestID")]
        public string CheckoutRequestID { get; set; }

        [JsonProperty("ResultCode")]
        public string ResultCode { get; set; }

        [JsonProperty("ResultDesc")]
        public string ResultDesc { get; set; }
    }
}
