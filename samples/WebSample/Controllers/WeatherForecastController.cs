using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mpesa.SDK.AspNetCore;

namespace WebSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILipaNaMpesa lnm;
        private readonly IC2B c2B;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILipaNaMpesa lnm, IC2B c2b, ILogger<WeatherForecastController> logger)
        {
            this.lnm = lnm;
            c2B = c2b;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stk = await lnm.PushStk("254722000000", "50", "My Account");
            if (!stk.Success)
            {
                _logger.LogError("Stk Error: @{stk}", stk.Error);
                return BadRequest(stk.Error);
            }

            var query = await lnm.QueryStatus(stk.Data.CheckoutRequestID);
            if (!query.Success)
            {
                _logger.LogError("QUery Error: @{query}", query.Error);
                return BadRequest(query.Error);
            }

            var c2bBalance = await c2B.QueryBalance();
            if (!c2bBalance.Success)
                _logger.LogError("C2B Balance Error: @{c2bBalance}", c2bBalance.Error);

            _logger.LogInformation("C2B Balance Completed: @{stk}", c2bBalance.Data);

            return Ok(stk.Data);
        }
    }
}
