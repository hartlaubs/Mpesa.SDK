using Microsoft.AspNetCore.Mvc;
using Mpesa.SDK.AspNetCore.Callbacks;
using System;

namespace Mpesa.SDK.AspNetCore.Binders
{
    [ModelBinder(BinderType = typeof(B2CModelBinder))]
    public class B2CCallbackResponse
        : BaseCallbackResponse<B2CResponse, AccountError> { }

    public class B2CModelBinder : BaseModelBinder<B2CCallbackResponse, ResultResponse>
    {
        protected override B2CCallbackResponse GetCallback(ResultResponse request)
        {
            if (request.Result == null)
                throw new ArgumentNullException(nameof(request.Result));

            var callback = new B2CCallbackResponse();
            if (request.Result.ResultCode == 0)
                callback.Data = B2CResponse.From(request.Result);
            else
                callback.Error = AccountError.From(request.Result);

            return callback;
        }
    }
}
