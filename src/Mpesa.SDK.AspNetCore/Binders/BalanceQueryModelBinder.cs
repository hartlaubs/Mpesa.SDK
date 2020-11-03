using Microsoft.AspNetCore.Mvc;
using Mpesa.SDK.AspNetCore.Callbacks;
using System;

namespace Mpesa.SDK.AspNetCore.Binders
{
    [ModelBinder(BinderType = typeof(BalanceQueryModelBinder))]
    public class BalanceQueryCallbackResponse
        : BaseCallbackResponse<BalanceQueryResponse, AccountError> { }

    public class BalanceQueryModelBinder : BaseModelBinder<BalanceQueryCallbackResponse, ResultResponse>
    {
        protected override BalanceQueryCallbackResponse GetCallback(ResultResponse request)
        {
            if (request.Result == null)
                throw new ArgumentNullException(nameof(request.Result));

            var callback = new BalanceQueryCallbackResponse();
            if (request.Result.ResultCode == 0)
                callback.Data = BalanceQueryResponse.From(request.Result);
            else
                callback.Error = AccountError.From(request.Result);

            return callback;
        }
    }
}
