using Microsoft.AspNetCore.Mvc;
using Mpesa.SDK.AspNetCore.Callbacks;
using System;

namespace Mpesa.SDK.AspNetCore.Binders
{
    [ModelBinder(BinderType = typeof(C2BModelBinder))]
    public class C2BCallbackResponse
        : BaseCallbackResponse<C2BResponse, C2BResponse> { }

    public class C2BModelBinder : BaseModelBinder<C2BCallbackResponse, C2BResult>
    {
        protected override C2BCallbackResponse GetCallback(C2BResult request)
        {
            if (request.BillRefNumber == null)
                throw new ArgumentNullException(nameof(request.BillRefNumber));

            var callback = new C2BCallbackResponse
            {
                Data = C2BResponse.From(request)
            };

            return callback;
        }
    }
}
