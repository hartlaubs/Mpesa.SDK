using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mpesa.SDK.AspNetCore.Binders;

namespace Mpesa.SDK.AspNetCore.Extensions
{
    public class MpesaConfigureOptions : IConfigureOptions<MvcOptions>
    {
        public void Configure(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new MpesaModelBinderProvider());
        }
    }
}
