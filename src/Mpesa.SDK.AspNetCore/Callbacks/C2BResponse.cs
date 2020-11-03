using System;
using System.Globalization;

namespace Mpesa.SDK.AspNetCore.Callbacks
{
    public class C2BResponse
    {
        public string TransactionType { get; set; }

        public string TransID { get; set; }

        public DateTimeOffset TransTime { get; set; }

        public double TransAmount { get; set; }

        public string BusinessShortCode { get; set; }

        public string BillRefNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public string OrgAccountBalance { get; set; }

        public string ThirdPartyTransID { get; set; }

        public string MSISDN { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public static C2BResponse From(C2BResult result)
        {
            return new C2BResponse
            {
                TransactionType = result.TransactionType,
                TransID = result.TransID,
                TransTime = DateTimeOffset.ParseExact(result.TransTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                TransAmount = double.Parse(result.TransAmount),
                BusinessShortCode = result.BusinessShortCode,
                BillRefNumber = result.BillRefNumber,
                InvoiceNumber = result.InvoiceNumber,
                OrgAccountBalance = result.OrgAccountBalance,
                ThirdPartyTransID = result.ThirdPartyTransID,
                MSISDN = result.MSISDN,
                FirstName = result.FirstName,
                MiddleName = result.MiddleName,
                LastName = result.LastName
            };
        }
    }

    public class C2BResult
    {
        public string TransactionType { get; set; }

        public string TransID { get; set; }

        public string TransTime { get; set; }

        public string TransAmount { get; set; }

        public string BusinessShortCode { get; set; }

        public string BillRefNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public string OrgAccountBalance { get; set; }

        public string ThirdPartyTransID { get; set; }

        public string MSISDN { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }
    }
}
