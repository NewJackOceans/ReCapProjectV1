using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Requests.CarTyreChanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarTyreChangeManager : ICarTyreChangeService
    {
        ICarTyreChangeDal _carTyreChangeDal;
        ICarService _carService;
        ITyreService _tyreService;

        public CarTyreChangeManager(ICarTyreChangeDal carTyreChangeDal, ICarService carService, ITyreService tyreService)
        {
            _carTyreChangeDal = carTyreChangeDal;
            _carService = carService;
            _tyreService = tyreService;
        }

        public IResult Add(CreateCarTyreChangeRequest request)
        {
            CarTyreChange carTyreChange = new CarTyreChange();

            var car = _carService.GetById(request.CarId);
            if (!car.Success)
                return new ErrorResult(car.Message);
            var tyre = _tyreService.GetById(request.TyreId);
            if (!tyre.Success)
                return new ErrorResult(tyre.Message);

            carTyreChange.CarId = request.CarId;
            
            carTyreChange.TyreId = request.TyreId;
            carTyreChange.TyreChangeDate = DateTime.Now;
            carTyreChange.TyreChangeKm = request.TyreChangeKm;

            _carTyreChangeDal.Add(carTyreChange);
            return new SuccessResult(Messages.CarTyreChangeAdded);


        }

        public IResult Delete(int id)
        {
            var carTyreChange = _carTyreChangeDal.Get(carTyreChange => carTyreChange.Id == id);
            if (carTyreChange != null)
                _carTyreChangeDal.Delete(carTyreChange);
            return new SuccessResult(Messages.CarTyreChangeDeleted);
        }

        public IDataResult<List<CarTyreChange>> GetAll()
        {
            return new SuccessDataResult<List<CarTyreChange>>(_carTyreChangeDal.GetAll(), Messages.CarTyreChangeListed);
        }

        public IDataResult<List<CarTyreChange>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<CarTyreChange>>(_carTyreChangeDal.GetForPageable(null, pageIndex, pageCount));
        }

        public Pageable<CarTyreChange> Search(int id, int carId, int tyreId, int tyreChangeKm, DateTime tyreChangeDate, int pageIndex, int pageCount)
        {
            Expression<Func<CarTyreChange, bool>> searchQuery = carTyreChange =>
            (id > 0 ? carTyreChange.Id == id : true) &&
            (carId > 0 ? carTyreChange.CarId == carId : true) &&
            (tyreId > 0 ? carTyreChange.TyreId == tyreId : true) &&
            (tyreChangeKm > 0 ? carTyreChange.TyreChangeKm == tyreChangeKm : true) &&
            (tyreChangeDate == null ? carTyreChange.TyreChangeDate == tyreChangeDate : true);

            var carTyreChanges = _carTyreChangeDal.GetForPageable(searchQuery, pageIndex, pageCount);
            var count = _carTyreChangeDal.GetCount(searchQuery);
            var data = new Pageable<CarTyreChange>(pageIndex, pageCount, count, carTyreChanges);

            return data;

        }

        public IResult Update(int id, UpdateCarTyreChangeRequest request)
        {
            var carTyreChange = _carTyreChangeDal.Get(carTyreChange => carTyreChange.Id == id);
            if (carTyreChange != null)
            {
                var car = _carService.GetById(id);
                if (!car.Success)
                    return new ErrorResult(car.Message);

                var tyre = _tyreService.GetById(id);
                if (!tyre.Success)
                    return new ErrorResult(tyre.Message);

                carTyreChange.CarId = request.CarId;
                carTyreChange.TyreId = request.TyreId;
                carTyreChange.TyreChangeDate = request.TyreChangeDate;
                carTyreChange.TyreChangeKm = request.TyreChangeKm;

                _carTyreChangeDal.Update(carTyreChange);
                return new SuccessResult(Messages.TyreCarChangeUpdated);

            }
            return new ErrorResult(Messages.TyreCarChangeNotUpdated);
        }
    }
}
