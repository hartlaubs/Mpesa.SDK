namespace Mpesa.SDK.AspNetCore.Callbacks
{
    public class AccountError : BaseResponse
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public static AccountError From(Result result)
        {
            return new AccountError
            {
                OriginatorConversationID = result.OriginatorConversationID,
                ConversationID = result.ConversationID,
                TransactionID = result.TransactionID,
                ErrorCode = result.ResultCode,
                ErrorMessage = result.ResultDesc
            };
        }
    }
}
