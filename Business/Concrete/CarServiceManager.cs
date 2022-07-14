using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Requests.CarServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarServiceManager : ICarServiceService
    {
        private ICarServiceDal _carServiceDal;
        ICarService _carService;

        public CarServiceManager(ICarServiceDal carServiceDal, ICarService carService)
        {
            _carServiceDal = carServiceDal;
            _carService = carService;
        }

        public IResult Add(CreateCarServiceRequest request)
        {
            CarService carService = new CarService();

            var car = _carService.GetById(request.CarId);
            if (!car.Success)
                return new ErrorResult(car.Message);
            if (request.ServiceEntryDate > request.ServiceExitDate )
                return new ErrorResult(Messages.ServiceEntryCannotBeGreaterThanServiceExitDate);

            carService.CarId = request.CarId;
            carService.Description = request.Description;
            carService.Km = request.Km;
            carService.ServiceEntryDate = request.ServiceEntryDate;
            carService.ServiceExitDate = request.ServiceExitDate;
            carService.ServiceType = request.ServiceType;

            _carServiceDal.Add(carService);
            return new SuccessResult(Messages.CarServiceAdded);


        }

        public IResult Delete(int id)
        {
            var carService = _carServiceDal.Get(carService => carService.Id == id);
            if (carService != null)
                _carServiceDal.Delete(carService);
            return new SuccessResult(Messages.CarServiceDeleted);
        }

        public IDataResult<List<CarService>> GetAll()
        {
            return new SuccessDataResult<List<CarService>>(_carServiceDal.GetAll(), Messages.CarServiceListed);
        }

        

        public IDataResult<CarService> GetById(int id)
        {
            var carService = _carServiceDal.Get(carService => carService.Id == id);
            if (carService == null)
            {
                return new ErrorDataResult<CarService>(Messages.NotFoundCarService);
            }
            return new SuccessDataResult<CarService>(carService);                

        }

        public IDataResult<List<CarService>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<CarService>>(_carServiceDal.GetForPageable(null, pageIndex, pageCount), Messages.CarServicePaging);
        }

        public Pageable<CarService> Search(int carId, string serviceType, DateTime serviceEntryDate, DateTime serviceExitDate, int pageIndex, int pageCount)
        {
            Expression<Func<CarService, bool>> searhQuery = carService =>
            (carId > 0 ? carService.CarId == carId : true) &&
            (!string.IsNullOrWhiteSpace(serviceType) ? carService.ServiceType.Contains(serviceType) : true) &&
            (serviceEntryDate == null ? carService.ServiceEntryDate == serviceEntryDate : true) &&
            (serviceExitDate == null ? carService.ServiceExitDate == serviceExitDate : true);

            var carServices = _carServiceDal.GetForPageable(searhQuery, pageIndex, pageCount);
            var count = _carServiceDal.GetCount(searhQuery);
            var data = new Pageable<CarService>(pageIndex, pageCount, count, carServices);

            return data;

        }

        public IResult Update(int id, UpdateCarServiceRequest request)
        {
            var carService = _carServiceDal.Get(carService => carService.Id == id);
            if (carService != null)
            {
                var car = _carService.GetById(id);
                if (car.Success)
                    carService.CarId = request.CarId;
                else
                    return new ErrorResult(car.Message);

                carService.Description = request.Description;
                carService.Km = request.Km;
                carService.ServiceEntryDate = request.ServiceEntryDate;
                carService.ServiceExitDate = request.ServiceExitDate;
                carService.ServiceType = request.ServiceType;
            }
            return new ErrorResult(Messages.CarServiceNotUpdated);

        }
    }
}
