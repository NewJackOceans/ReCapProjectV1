using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests.CarTyreChanges
{
    public class CreateBulkNameCarTyreChangeRequest
    {
        public List<string> TyreNames { get; set; }
        public string CarName { get; set; }
        public int TyreChangeKm { get; set; }
    }
}
