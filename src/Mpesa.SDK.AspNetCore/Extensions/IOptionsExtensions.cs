namespace Mpesa.SDK.AspNetCore
{
    public static class IOptionsExtensions
    {
        public static MpesaApiOptions ToMpesaOptions(this LipaNaMpesaOptions options)
        {
            return new MpesaApiOptions
            {
                ShortCode = options.ShortCode,
                PassKey = options.PassKey,
                IsLive = options.IsLive,
                ResultURL = options.CallbackURL
            };
        }

        public static MpesaApiOptions ToMpesaOptions<T>(this T options) where T : Options
        {
            return new MpesaApiOptions
            {
                ShortCode = options.ShortCode,
                Initiator = options.Initiator,
                InitiatorPassword = options.InitiatorPassword,
                IsLive = options.IsLive,
                ResultURL = options.ResultURL,
                QueueTimeOutURL = options.QueueTimeOutURL
            };
        }
    }
}
