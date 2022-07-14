using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests.TyreCategories
{
    public class UpdateTyreCategoryRequest
    {
        public string TyreCategoryName { get; set; }
        public string TyreSpeedIndex { get; set; }
    }
}
