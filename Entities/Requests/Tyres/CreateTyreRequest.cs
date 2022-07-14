using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests.Tyres
{
    public class CreateTyreRequest
    {
        public string TyreName { get; set; }
        public int TyreCategoryId { get; set; }
        public string Description { get; set; }
        public int TyreBrandId { get; set; }
    }
}
