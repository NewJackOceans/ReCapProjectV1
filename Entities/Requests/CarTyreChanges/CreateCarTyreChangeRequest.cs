using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests.CarTyreChanges
{
    public class CreateCarTyreChangeRequest
    {
        public int CarId { get; set; }
        public int TyreBrandId { get; set; }
        public DateTime TyreChangeDate { get; set; }
        public int TyreChangeKm { get; set; }
    }
}
