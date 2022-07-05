using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests.Payments
{
    public class UpdatePaymentRequest
    {
        public int CustomerId { get; set; }
        public string CreditCardNumber { get; set; }
        public decimal Price { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
    }
}
