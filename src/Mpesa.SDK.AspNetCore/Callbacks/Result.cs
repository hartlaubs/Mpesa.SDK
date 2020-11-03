namespace Mpesa.SDK.AspNetCore.Callbacks
{
    public class ResultResponse
    {
        public Result Result { get; set; }
    }

    public class Result
    {
        public int ResultType { get; set; }

        public int ResultCode { get; set; }

        public string ResultDesc { get; set; }

        public string OriginatorConversationID { get; set; }

        public string ConversationID { get; set; }

        public string TransactionID { get; set; }

        public ResultParameters ResultParameters { get; set; }

        public ReferenceData ReferenceData { get; set; }
    }

    public class ResultParameters
    {
        public object ResultParameter { get; set; }
    }

    public class ReferenceData
    {
        public KeyValuePair ReferenceItem { get; set; }
    }

    public class KeyValuePair
    {
        public string Key { get; set; }

        public object Value { get; set; }
    }
}
