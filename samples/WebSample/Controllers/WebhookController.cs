using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mpesa.SDK.AspNetCore.Binders;

namespace WebSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger logger;

        public WebhookController(ILogger<WebhookController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Receives Callback for Balance check for both C2B and B2C requests
        /// </summary>
        /// <param name="requestId">Randomly generated request Id</param>
        /// <param name="response">Balance Payload</param>
        /// <returns></returns>
        [HttpPost("{requestId}/balance")]
        public IActionResult QueryBalanceCallback(string requestId, BalanceQueryCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("Balance Callback Data: {@Balance}", response.Data);
            else
                logger.LogError("Balance Callback Error: {@Error}", response.Error);

            return Ok(new { ResultCode = "00000000", ResponseDesc = "success" });
        }

        /// <summary>
        /// Receives Callback for Query Transaction status for both C2B and B2C requests
        /// </summary>
        /// <param name="requestId">Randomly generated request Id</param>
        /// <param name="response">Status query payload</param>
        /// <returns></returns>
        [HttpPost("{requestId}/status")]
        public IActionResult QueryStatusCallback(string requestId, StatusQueryCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("Transaction Status Callback Data: {@Data}", response.Data);
            else
                logger.LogError("Transaction Status Callback Error: {@Error}", response.Error);

            return Ok(new { ResultCode = "00000000", ResponseDesc = "success" });
        }

        /// <summary>
        /// Receives Callback for Reversal of transaction for both C2B and B2C Request
        /// </summary>
        /// <param name="requestId">Randomly generated request Id</param>
        /// <param name="response">Reversal callback payload</param>
        /// <returns></returns>
        [HttpPost("{requestId}/reversal")]
        public IActionResult ReverseCallback(string requestId, ReverseCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("Reverse Transaction Callback Data: {@Data}", response.Data);
            else
                logger.LogError("Reverse transaction Callback Error: {@Error}", response.Error);

            return Ok(new { ResultCode = "00000000", ResponseDesc = "success" });
        }

        /// <summary>
        /// Receives callback for money send through B2C
        /// </summary>
        /// <param name="requestId">Randomly generated request Id</param>
        /// <param name="response">B2C Payment payload</param>
        /// <returns></returns>
        [HttpPost("{requestId}/b2c")]
        public IActionResult B2CCallback(string requestId, B2CCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("B2C Callback Data: {@Data}", response.Data);
            else
                logger.LogError("B2C Callback Error: {@Error}", response.Error);

            return Ok(new { ResultCode = "00000000", ResponseDesc = "success" });
        }

        /// <summary>
        /// Receives callback for payments done through stk push
        /// </summary>
        /// <param name="requestId">Randomly generated request Id</param>
        /// <param name="response">Push payment payload</param>
        /// <returns></returns>
        [HttpPost("{requestId}/lnm")]
        public IActionResult LipaNaMpesaCallback(string requestId, LipaNaMpesaCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("Lipa Na Mpesa Callback Data: {@Data}", response.Data);
            else
                logger.LogError("Lipa Na Mpesa Callback Error: {@Error}", response.Error);

            return Ok(new { ResultCode = "00000000", ResponseDesc = "success" });
        }

        /// <summary>
        /// C2B Payment and confirmation can be processed here
        /// </summary>
        /// <param name="response">Response Payload</param>
        /// <returns></returns>
        [HttpPost("c2b/confirmation")]
        public IActionResult C2BPaymentCallback(C2BCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("C2B Payment Confirmation: {@Data}", response.Data);

            return Ok(new { ResultCode = "00000000", ResponseDesc = "success" });
        }
    }
}
