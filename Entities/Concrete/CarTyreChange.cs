using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CarTyreChange : IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int TyreBrandId { get; set; }
        public DateTime TyreChangeDate { get; set; }
        public int TyreChangeKm { get; set; }

    }
}
