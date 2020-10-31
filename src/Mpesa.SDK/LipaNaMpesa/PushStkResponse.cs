using System;
using System.Collections.Generic;
using System.Text;

namespace Mpesa.SDK.LipaNaMpesa
{
    public class PushStkResponse : Response
    {
        /// <summary>
        /// Gets or sets the checkout request identifier.
        /// </summary>
        /// <value>The checkout request identifier.</value>
        public string CheckoutRequestID { get; set; }

        /// <summary>
        /// Gets or sets the customer message.
        /// </summary>
        /// <value>The customer message.</value>
        public string CustomerMessage { get; set; }

        /// <summary>
        /// Gets or sets the merchant request identifier.
        /// </summary>
        /// <value>The merchant request identifier.</value>
        public string MerchantRequestID { get; set; }
    }
}
