using Microsoft.AspNetCore.Mvc;
using Mpesa.SDK.AspNetCore.Callbacks;
using System;

namespace Mpesa.SDK.AspNetCore.Binders
{
    [ModelBinder(BinderType = typeof(StatusQueryModelBinder))]
    public class StatusQueryCallbackResponse
        : BaseCallbackResponse<StatusQueryResponse, AccountError> { }

    public class StatusQueryModelBinder : BaseModelBinder<StatusQueryCallbackResponse, ResultResponse>
    {
        protected override StatusQueryCallbackResponse GetCallback(ResultResponse request)
        {
            if (request.Result == null)
                throw new ArgumentNullException(nameof(request.Result));

            var callback = new StatusQueryCallbackResponse();
            if (request.Result.ResultCode == 0)
                callback.Data = StatusQueryResponse.From(request.Result);
            else
                callback.Error = AccountError.From(request.Result);

            return callback;
        }
    }
}
