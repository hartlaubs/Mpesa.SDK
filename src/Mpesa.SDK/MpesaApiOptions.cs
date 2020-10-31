using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Mpesa.SDK
{
    public class MpesaApiOptions
    {
        public string ShortCode { get; set; }
        public string Initiator { get; set; }
        public string InitiatorPassword { get; set; }
        public string PassKey { get; set; }
        public bool IsLive { get; set; }
        public string QueueTimeOutURL { get; set; }
        public string ResultURL { get; set; }
    }

    public class Options : MpesaApiOptions
    {
        private Options() { }

        public static Options From(MpesaApiOptions options)
        {
            return new Options
            {
                ShortCode = options.ShortCode,
                Initiator = options.Initiator,
                InitiatorPassword = options.InitiatorPassword,
                PassKey = options.PassKey,
                IsLive = options.IsLive,
                QueueTimeOutURL = options.QueueTimeOutURL,
                ResultURL = options.ResultURL
            };
        }

        public string BaseUri => IsLive ? Settings.ApiBaseUri_Production : Settings.ApiBaseUri_Test;
        public string AuthUri => IsLive ? Settings.AuthBaseUri_production : Settings.AuthBaseUri_Test;
        public string SecurityCredential => Encrypt(InitiatorPassword);
        public string EncodedPassword => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ShortCode}{PassKey}{Timestamp}"));
        public string Timestamp => DateTime.Now.ToString("yyyyMMddhhmmss");

        private string Encrypt(string password)
        {
            var cerificate = IsLive ? "production.cer" : "sandbox.cer";
            var assembly = typeof(MpesaApiOptions).GetTypeInfo().Assembly;
            var resource = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Certs.{cerificate}");
            using var memoryStream = new MemoryStream();
            resource.CopyTo(memoryStream);
            var cert = new X509Certificate2(memoryStream.ToArray());
            using var rsa = cert.GetRSAPublicKey();
            var data = rsa.Encrypt(Encoding.UTF8.GetBytes(password), RSAEncryptionPadding.Pkcs1);

            return Convert.ToBase64String(data);
        }
    }
}
