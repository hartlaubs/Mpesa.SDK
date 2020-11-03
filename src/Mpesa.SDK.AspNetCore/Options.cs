namespace Mpesa.SDK.AspNetCore
{
    public class Options
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string ShortCode { get; set; }
        public string Initiator { get; set; }
        public string InitiatorPassword { get; set; }
        public bool IsLive { get; set; }
        public string CallbackURL { get; set; }
    }
}
