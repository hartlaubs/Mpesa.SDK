using Microsoft.AspNetCore.Mvc;
using Mpesa.SDK.AspNetCore.Callbacks;
using System;

namespace Mpesa.SDK.AspNetCore.Binders
{
    [ModelBinder(BinderType = typeof(LipaNaMpesaModelBinder))]
    public class LipaNaMpesaCallbackResponse
       : BaseCallbackResponse<LipaNaMpesaResponse, LipaNaMpesaError> { }

    public class LipaNaMpesaModelBinder : BaseModelBinder<LipaNaMpesaCallbackResponse, BodyResponse>
    {
        protected override LipaNaMpesaCallbackResponse GetCallback(BodyResponse request)
        {
            if (request.Body == null)
                throw new ArgumentNullException(nameof(request.Body));

            var callback = new LipaNaMpesaCallbackResponse();
            if (request.Body.stkCallback.ResultCode == 0)
                callback.Data = LipaNaMpesaResponse.From(request.Body.stkCallback);
            else
                callback.Error = LipaNaMpesaError.From(request.Body.stkCallback);

            return callback;
        }
    }
}
