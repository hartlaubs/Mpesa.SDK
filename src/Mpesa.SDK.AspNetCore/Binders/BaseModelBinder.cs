using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mpesa.SDK.AspNetCore.Extensions;
using Newtonsoft.Json;
using System;
using System.IO.Pipelines;
using System.Threading.Tasks;

namespace Mpesa.SDK.AspNetCore.Binders
{
    public abstract class BaseModelBinder<T, TResponse> : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext context)
        {
            ReadResult res = new ReadResult();
            try
            {
                if (context == null)
                    throw new ArgumentNullException(nameof(context));

                res = await context.HttpContext.Request.BodyReader.ReadAsync();
                var body = res.Buffer.GetString();
                var request = JsonConvert.DeserializeObject<TResponse>(body);
                if (request == null) return;
                var callback = GetCallback(request);
                context.Result = ModelBindingResult.Success(callback);
            }
            catch
            {
                context.Result = ModelBindingResult.Failed();
            }
            finally
            {
                context.HttpContext.Request.BodyReader.AdvanceTo(res.Buffer.Start, res.Buffer.Start);
            }
        }

        protected abstract T GetCallback(TResponse request);
    }
}
