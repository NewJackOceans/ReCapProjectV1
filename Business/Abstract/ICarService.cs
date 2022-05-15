using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll();
        List<Car> GetAllByCarId(int id);
        List<Car> GetByDailyPrice(int dailyPrice);
        public void Add(Car car);
        List<CarDetailDto> GetCarDetails()


    }
}
