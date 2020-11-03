namespace Mpesa.SDK.AspNetCore.Callbacks
{
    public class ReverseResponse : BaseResponse
    {
        public string ResultDesc { get; set; }

        public static ReverseResponse From(Result result)
        {
            return new ReverseResponse
            {
                OriginatorConversationID = result.OriginatorConversationID,
                ConversationID = result.ConversationID,
                TransactionID = result.TransactionID,
                ResultDesc = result.ResultDesc
            };
        }

    }
}
