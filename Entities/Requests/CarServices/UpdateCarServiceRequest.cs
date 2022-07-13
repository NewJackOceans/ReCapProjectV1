using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests.CarServices
{
    public class UpdateCarServiceRequest
    {
        public int CarId { get; set; }
        public string Description { get; set; }
        public string ServiceType { get; set; }
        public int Km { get; set; }
        public DateTime ServiceEntryDate { get; set; }
        public DateTime ServiceExitDate { get; set; }
    }
}
