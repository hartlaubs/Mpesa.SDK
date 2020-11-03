using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mpesa.SDK.AspNetCore;

namespace WebSample.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILipaNaMpesa lnm;
        private readonly IC2B c2b;
        private readonly IB2C b2c;
        private readonly ILogger _logger;

        public HomeController(ILipaNaMpesa lnm, IC2B c2b, IB2C b2c, ILogger<HomeController> logger)
        {
            this.lnm = lnm;
            this.c2b = c2b;
            this.b2c = b2c;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // STK Push
            var stk = await lnm.PushStk("254712345678", "10", "My Account");
            if (!stk.Success)
                _logger.LogError("Stk Push Error: {@Error}", stk.Error);
            else
                _logger.LogInformation("Stk Push Data: {@Data}", stk.Data);

            // Wait to prevent System busy error
            await Task.Delay(2000);

            // STK Query
            var query = await lnm.QueryStatus("ws_CO_031120201658538238");
            if (!query.Success)
                _logger.LogError("Stk Query Error: {@Error}", query.Error);
            else
                _logger.LogInformation("Stk Query Data: {@Data}", query.Data);

            // Wait to prevent System busy error
            await Task.Delay(2000);

            // Check C2B Account Balance. Can also do forn B2C.
            var c2bBalance = await c2b.QueryBalance();
            if (!c2bBalance.Success)
                _logger.LogError("C2B Balance Error: {@Error}", c2bBalance.Error);
            else
                _logger.LogInformation("C2B Balance Data: {@Data}", c2bBalance.Data);

            // Wait to prevent System busy error
            await Task.Delay(2000);

            // Check C2B transaction status. Can also do forn B2C
            var c2bStatus = await c2b.QueryTransactionStatus("OJQERYZFSR", Mpesa.SDK.Account.IdentifierTypeEnum.Organization);
            if (!c2bStatus.Success)
                _logger.LogError("Transaction Status Error: {@Error}", c2bStatus.Error);
            else
                _logger.LogInformation("Transaction Status Data: {@Data}", c2bStatus.Data);

            // Wait to prevent System busy error
            await Task.Delay(2000);

            // Reverse C2B Transaction. Can also do forn B2C
            var reverse = await c2b.RequestReversal("OFRJUI4TAZ", "700");
            if (!reverse.Success)
                _logger.LogError("Transaction reversal Error: {@Error}", reverse.Error);
            else
                _logger.LogInformation("Transaction reversal Data: {@Data}", reverse.Data);

            // Wait to prevent System busy error
            await Task.Delay(2000);

            // Send Money through B2C
            var sendB2C = await b2c.SendMoney("254712345678", "27.28");
            if (!sendB2C.Success)
                _logger.LogError("B2C Send Error: {@Error}", sendB2C.Error);
            else
                _logger.LogInformation("B2C Send Data: {@Data}", sendB2C.Data);


            return Ok("Ok");
        }
    }
}
