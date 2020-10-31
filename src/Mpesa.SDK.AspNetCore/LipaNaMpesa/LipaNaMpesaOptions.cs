namespace Mpesa.SDK.AspNetCore
{
    public class LipaNaMpesaOptions
    {
        public const string Name = "LipaNaMpesa";

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string ShortCode { get; set; }
        public string PassKey { get; set; }
        public bool IsLive { get; set; }
        public string CallbackURL { get; set; }
    }
}
