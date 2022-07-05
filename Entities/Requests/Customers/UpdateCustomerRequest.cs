using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests.Customers
{
    public class UpdateCustomerRequest
    {
        public int UserId { get; set; }
        public string CompanyName { get; set; }
    }
}
