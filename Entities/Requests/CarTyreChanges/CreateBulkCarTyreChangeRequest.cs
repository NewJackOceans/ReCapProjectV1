using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests.CarTyreChanges
{
    public class CreateBulkCarTyreChangeRequest
    {
        public int CarId { get; set; }        
        public List<int> TyreIds { get; set; }
        public int TyreChangeKm { get; set; }
    }
}
