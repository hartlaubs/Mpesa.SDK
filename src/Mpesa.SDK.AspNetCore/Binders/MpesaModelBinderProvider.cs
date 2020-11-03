using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

namespace Mpesa.SDK.AspNetCore.Binders
{
    public class MpesaModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(StatusQueryCallbackResponse))
                return new BinderTypeModelBinder(typeof(StatusQueryModelBinder));

            if (context.Metadata.ModelType == typeof(BalanceQueryCallbackResponse))
                return new BinderTypeModelBinder(typeof(BalanceQueryModelBinder));

            if (context.Metadata.ModelType == typeof(ReverseCallbackResponse))
                return new BinderTypeModelBinder(typeof(ReverseModelBinder));

            if (context.Metadata.ModelType == typeof(B2CCallbackResponse))
                return new BinderTypeModelBinder(typeof(B2CModelBinder));

            if (context.Metadata.ModelType == typeof(LipaNaMpesaCallbackResponse))
                return new BinderTypeModelBinder(typeof(LipaNaMpesaModelBinder));

            if (context.Metadata.ModelType == typeof(C2BCallbackResponse))
                return new BinderTypeModelBinder(typeof(C2BModelBinder));

            return null;
        }
    }
}
