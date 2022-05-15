using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car car)
        {
            
        }

        public List<Car> GetAll()
        {
            //İş kodları
            //Yetkisi var mı?
            return _carDal.GetAll();

        }

        public List<Car> GetAllByCarId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetByDailyPrice(int dailyPrice)
        {
            throw new NotImplementedException();
        }

        public Car GetById(int carId)
        {
            return _carDal.Get(c=>c.Id == carId);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            return _carDal.GetCarDetails();
        }
    }
}
