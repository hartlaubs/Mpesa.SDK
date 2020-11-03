namespace Mpesa.SDK.AspNetCore.Callbacks
{
    public class BaseCallbackResponse<T, TError>
    {
        public T Data { get; set; }

        public bool Success => Error == null;

        public TError Error { get; set; }
    }
}
