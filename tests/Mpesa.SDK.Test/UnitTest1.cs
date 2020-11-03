using System;
using System.Threading.Tasks;
using Xunit;

namespace Mpesa.SDK.Test
{
    public class UnitTest1
    {
        private readonly MpesaApiOptions options;

        private readonly MpesaApi api;

        public UnitTest1()
        {
            options = new MpesaApiOptions
            {
                ShortCode = "",
                Initiator = "",
                InitiatorPassword = "",
                PassKey = "",
                IsLive = false,
                ResultURL = "",
                QueueTimeoutURL = ""
            };
            api = new MpesaApi("W77IT5mNOnBwkUwWhNxAKPS61uZur3pj", "qGkYG0ZUkfXFIRZE", options);
        }

        [Fact]
        public async Task CheckAuth()
        {
            var result = await api.Auth.GetAccessToken();
        }
    }
}
