using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Performance;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Requests.Cars;
using System.Linq;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService;
        IColorService _colorService;


        public CarManager(ICarDal carDal, IBrandService brandService, IColorService colorService)
        {
            _carDal = carDal;
            _brandService = brandService;
            _colorService = colorService;

        }
        [SecuredOperation("admin")]
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

        //[PerformanceAspect(5)]
        //[SecuredOperation("car.add,admin")]
        //[ValidationAspect(typeof(CarValidator))]
        //[CacheRemoveAspect("ICarService.Get")]
        public IResult Add(CreateCarRequest request)
        {

            //ValidationTool.Validate(new CarValidator(), car);
            Car car = new Car();
            var brand = _brandService.GetById(request.BrandId);
            if (!brand.Success)
                return new ErrorResult(brand.Message);

            var color = _colorService.GetById(request.ColorId);
            if (!color.Success)
                return new ErrorResult(color.Message);

            car.ColorId = request.ColorId;
            car.BrandId = request.BrandId;
            car.CarName = request.CarName;
            car.DailyPrice = request.DailyPrice;
            car.ModelYear = request.ModelYear;
            car.Description = request.Description;

            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        //[ValidationAspect(typeof(CarValidator))]
        //[CacheRemoveAspect("ICarService.Get")]
        public IResult Update(int id, UpdateCarRequest request)
        {
            var car = _carDal.Get(car => car.CarId == id);
            if (car != null)
            {
                var brand = _brandService.GetById(id);
                if (!brand.Success)
                    return new ErrorResult(brand.Message);

                var color = _colorService.GetById(id);
                if (!color.Success)
                    return new ErrorResult(Messages.NotFoundColor);

                car.ColorId = request.ColorId;
                car.BrandId = request.BrandId;
                car.CarName = request.CarName;
                car.ModelYear = request.ModelYear;
                car.DailyPrice = request.DailyPrice;
                car.Description = request.Description;
                _carDal.Update(car);
                return new SuccessResult(Messages.CarUpdated);
            }
            else
                return new ErrorResult(Messages.CarNotUpdated);

        }

        public IResult Delete(int id)
        {
            var car = _carDal.Get(car => car.CarId == id);
            if (car != null)
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

        public Pageable<Car> Search(string carName, string modelYear, int carId, int colorId, int brandId, int pageIndex, int pageCount)//Sayfalama
        {
            Expression<Func<Car, bool>> searchQuery = car =>
            (!string.IsNullOrWhiteSpace(modelYear) ? car.ModelYear.Contains(modelYear) : true) &&
            (!string.IsNullOrWhiteSpace(carName) ? car.CarName.Contains(carName) : true) &&
            (carId > 0 ? car.CarId == carId : true) &&
            (brandId > 0 ? car.BrandId == brandId : true) &&
            (colorId > 0 ? car.ColorId == colorId : true);


            var cars = _carDal.GetForPageable(searchQuery, pageIndex, pageCount);//Sayfalama
            var count = _carDal.GetCount(searchQuery);
            var data = new Pageable<Car>(pageIndex, pageCount, count, cars);

            return data;
        }

        public IDataResult<Car> GetById(int id)
        {
            var car = _carDal.Get(car => car.CarId == id);
            if (car == null)
                return new ErrorDataResult<Car>(Messages.CarNotFound);
            else
                return new SuccessDataResult<Car>(car);
        }

        public IDataResult<Car> GetByNameForValue(string carName)
        {
            var carNameValue = _carDal.Get(carNameValue => carNameValue.CarName == carName);
            if (carNameValue == null)
            {
                return new ErrorDataResult<Car>(Messages.CarNotFound);
            }
            else
                return new SuccessDataResult<Car>(carNameValue);
        }
    }
}