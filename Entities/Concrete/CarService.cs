using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CarService : IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Description { get; set; }
        public string ServiceType { get; set; }
        public int Km { get; set; }
        public DateTime ServiceEntryDate { get; set; }
        public DateTime ServiceExitDate { get; set; }


    }
}
