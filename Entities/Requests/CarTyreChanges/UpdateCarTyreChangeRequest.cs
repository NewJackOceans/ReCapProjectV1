﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests.CarTyreChanges
{
    public class UpdateCarTyreChangeRequest
    {
        public int CarId { get; set; }
        public int TyreId { get; set; }
        public DateTime TyreChangeDate { get; set; }
        public int TyreChangeKm { get; set; }
    }
}
