[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
# Mpesa.SDK
Mpesa.SDK is a Library for .NET that implements the Daraja API which is documented at [Safaricom](https://developer.safaricom.co.ke/docs). It creates a simple interface that can be used to call the M-Pesa API.

Mpesa.SDK Nuget Packages
------------------------

| Package Name | .NET Standard | .NET Core App | Version |
| ------------ | :-----------: | :-----------: | :-----: |
| **Main** |
| [Mpesa.SDK](https://www.nuget.org/packages/Mpesa.SDK) | 2.0, 2.1 | - | [![NuGet Badge](https://buildstats.info/nuget/Mpesa.SDK)](https://www.nuget.org/packages/Mpesa.SDK) |
| **ASP.NET Core** |
| [Mpesa.SDK.AspNetCore](https://www.nuget.org/packages/Mpesa.SDK.AspNetCore) | - | 3.1 | [![NuGet Badge](https://buildstats.info/nuget/Mpesa.SDK.AspNetCore)](https://www.nuget.org/packages/Mpesa.SDK.AspNetCore) |

## .NET Usage

### Installation

```
PM> Install-Package Mpesa.SDK
```

### Usage

#### Create an MpesaApiOptions

```csharp
var options = new MpesaApiOptions
{
  ShortCode = "",
  Initiator = "",
  InitiatorPassword = "",
  PassKey = "",
  IsLive = true,
  QueueTimeOutURL = "",
  ResultURL = ""
};
```

#### Create the api

```csharp
var api = new MpesaApi("<Consumer Key>", "<Consumer Secret>", options);
```

#### Start making calls

Query the account balance

```csharp
var balance = await api.Account.QueryBalance();
if (!balance.Success)
  Console.WriteLine(balance.Error.ErrorMessage);
```

Query Transaction Status

 ```csharp
 var transaction = await api.Account.QueryTransactionStatus("OJ68HHO4H", IdentifierTypeEnum.Organization);
if (!transaction.Success)
  Console.WriteLine(transaction.Error.ErrorMessage);
 ```
 
 Reverse Transaction
 
 ```csharp
 var reverse = await api.Account.RequestReversal("OIT8YS6I8Y", "850", "Accounting error");
if (!reverse.Success)
  Console.WriteLine(reverse.Error.ErrorMessage);
 ```
 
 STK Push
 
 ```csharp
 var stkQuery = await api.LipaNaMpesa.QueryStatus("ws_CO_30102020004040278972");
if (!stkQuery.Success)
  Console.WriteLine(stkQuery.Error.ErrorMessage);
  
 Console.WriteLine(stkQuery.Data.CheckoutRequestID);
 ```
 
 B2C Send Money
 
 ```csharp
 var b2c = await api.B2CClient.SendMoney("254722000000", "1000");
if (!b2c.Success)
  Console.WriteLine(b2c.Error.ErrorMessage);
 ```

## ASP.NET Core Usage

### Installation

```
PM> Install-Package Mpesa.SDK.AspNetCore
```

### Usage

Library consists of the following interfaces:


**ILipaNaMpesa** - StkPush and QueryStatus  
**IC2B** - QueryBalance, QueryTransactionStatus, ReverseTransaction, RegisterUrl, SimulateTransaction  
**IB2C** - QueryBalance, QueryTransactionStatus, ReverseTransaction, SendMoney  
**IB2B** - To come soon  

#### Add configuration to appsettings.json. Only add configuration relevant to the interface you want to use.  

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "Mpesa": {
    "B2C": {
      "ConsumerKey": "",
      "ConsumerSecret": "",
      "ShortCode": "",
      "Initiator": "",
      "InitiatorPassword": "",
      "IsLive": false,
      "CallbackURL": "https://example.com/webhook"
    },
    "C2B": {
      "ConsumerKey": "",
      "ConsumerSecret": "",
      "ShortCode": "",
      "Initiator": "",
      "InitiatorPassword": "",
      "IsLive": false,
      "CallbackURL": "https://example.com/webhook"
    },
    "LipaNaMpesa": {
      "ConsumerKey": "",
      "ConsumerSecret": "",
      "ShortCode": "",
      "PassKey": "",
      "IsLive": false,
      "CallbackURL": "https://example.com/webhook"
    }
  }
}
```
The callback has a predefined ending. For Example  
Balance Callback ends with `/<requestId>/balance` the requestId will be randomly generated  
and will be present for storage before the callback responds.  
The `CallbackUrl` should always point to a controller.

#### Add to Startup class

```csharp
services.AddMpesaSdk(Configuration);
```

#### Use dependency injection with the interface you require
```csharp
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
```

The WebhookController
```csharp
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
```

