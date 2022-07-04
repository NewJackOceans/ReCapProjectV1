using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Performance;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService;

        public CarManager(ICarDal carDal, IBrandService brandService)
        {
            _carDal = carDal;
            _brandService = brandService;

        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        
        public IDataResult<List<Car>> GetForPageable(int pageIndex, int pageCount)
        {

            return new SuccessDataResult<List<Car>>(_carDal.GetForPageable(null, pageIndex, pageCount), Messages.CarPaging);
        }
        
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var result = new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.CarDetailsListed);
            return result;
        }

        [PerformanceAspect(5)]
        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {

            ValidationTool.Validate(new CarValidator(), car);

            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }


        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }


        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByCarId(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByCarId(carId));

        }

        public IDataResult<List<Car>> Search(string modelYear, int carId, int colorId, int brandId, int pageIndex, int pageCount)
        {
            Expression<Func<Car, bool>> searchQuery = car =>
            (!string.IsNullOrWhiteSpace(modelYear) ? car.ModelYear.Contains(modelYear) : true) &&
            (carId > 0 ? car.CarId == carId : true) &&
            (brandId > 0 ? car.BrandId == brandId : true) &&
            (colorId > 0 ? car.ColorId == colorId : true);
            return new SuccessDataResult<List<Car>>(_carDal.GetForPageable(searchQuery, pageIndex, pageCount), Messages.CarPaging);
        }
    }
}