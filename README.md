[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
# Mpesa.SDK
Mpesa.SDK is a Library for .NET that implements the Daraja API which is documented at [Safaricom](https://developer.safaricom.co.ke/docs). It creates a simple interface that can be used to call the M-Pesa API.

Mpesa.SDK Nuget Packages
------------------------

| Package Name | .NET Standard | .NET Core App | Version |
| ------------ | :-----------: | :-----------: | :-----: |
| **Main** |
| [Mpesa.SDK](https://www.nuget.org/packages/Mpesa.SDK) | 2.0, 2.1 | [![NuGet Badge](https://buildstats.info/nuget/Mpesa.SDK)](https://www.nuget.org/packages/Mpesa.SDK) |
| **ASP.NET Core** |
| [Mpesa.SDK.AspNetCore](https://www.nuget.org/packages/Mpesa.SDK.AspNetCore) | - | 3.1 | [![NuGet Badge](https://buildstats.info/nuget/Mpesa.SDK.AspNetCore)](https://www.nuget.org/packages/Mpesa.SDK.AspNetCore) |

## .NET App

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
