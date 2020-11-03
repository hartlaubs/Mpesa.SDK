using Microsoft.AspNetCore.Mvc;
using Mpesa.SDK.AspNetCore.Callbacks;
using System;

namespace Mpesa.SDK.AspNetCore.Binders
{
    [ModelBinder(BinderType = typeof(ReverseModelBinder))]
    public class ReverseCallbackResponse
        : BaseCallbackResponse<ReverseResponse, AccountError> { }

    public class ReverseModelBinder : BaseModelBinder<ReverseCallbackResponse, ResultResponse>
    {
        protected override ReverseCallbackResponse GetCallback(ResultResponse request)
        {
            if (request.Result == null)
                throw new ArgumentNullException(nameof(request.Result));

            var callback = new ReverseCallbackResponse();
            if (request.Result.ResultCode == 0)
                callback.Data = ReverseResponse.From(request.Result);
            else
                callback.Error = AccountError.From(request.Result);

            return callback;
        }
    }
}
