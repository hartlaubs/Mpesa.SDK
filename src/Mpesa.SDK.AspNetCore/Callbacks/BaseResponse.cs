namespace Mpesa.SDK.AspNetCore.Callbacks
{
    public class BaseResponse
    {
        public string OriginatorConversationID { get; set; }

        public string ConversationID { get; set; }

        public string TransactionID { get; set; }
    }
}
