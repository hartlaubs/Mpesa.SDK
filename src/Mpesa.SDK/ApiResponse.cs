using Newtonsoft.Json;

namespace Mpesa.SDK
{
    public class ApiResponse
    {
        public bool Success => Error == null;

        public ApiError Error { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }
    }

    public class ApiError
    {
        public string RequestId { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class Response : BaseResponse
    {
        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>The response code.</value>
        [JsonProperty("ResponseCode")]
        public int ResponseCode { get; set; }
    }

    public class BaseResponse
    {
        /// <summary>
        /// Gets or sets the conversation identifier.
        /// </summary>
        /// <value>The conversation identifier.</value>
        [JsonProperty("ConversationID")]
        public string ConversationId { get; set; }

        /// <summary>
        /// Gets or sets the originator coversation identifier.
        /// </summary>
        /// <value>The originator coversation identifier.</value>
        [JsonProperty("OriginatorConversationID")]
        public string OriginatorCoversationId { get; set; }

        /// <summary>
        /// Gets or sets the response description.
        /// </summary>
        /// <value>The response description.</value>
        [JsonProperty("ResponseDescription")]
        public string ResponseDescription { get; set; }

        /// <summary>
        /// Gets or Set the Request Id
        /// </summary>
        /// <value>App Generated Id used to track the response</value>
        public string RequestId { get; set; }
    }
}
