using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Requests.Tyres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TyreManager : ITyreService
    {
        ITyreDal _tyreDal;
        ITyreBrandService _tyreBrandService;
        ITyreCategoryService _tyreCategoryService;

        public TyreManager(ITyreDal tyreDal, ITyreBrandService tyreBrandService, ITyreCategoryService tyreCategoryService)
        {
            _tyreDal = tyreDal;
            _tyreBrandService = tyreBrandService;
            _tyreCategoryService = tyreCategoryService;
        }

        public IResult Add(CreateTyreRequest request)
        {
            Tyre tyre = new Tyre();

            var tyreBrand = _tyreBrandService.GetById(request.TyreBrandId);
            if (!tyreBrand.Success)
                return new ErrorResult(tyreBrand.Message);
            var tyreCategory = _tyreCategoryService.GetById(request.TyreCategoryId);
            if (!tyreCategory.Success)
                return new ErrorResult(tyreCategory.Message); 
            
            
            tyre.TyreBrandId = request.TyreBrandId;
            tyre.TyreCategoryId = request.TyreCategoryId;
            tyre.Description = request.Description;
            tyre.TyreName = request.TyreName;

            _tyreDal.Add(tyre);
            return new SuccessResult(Messages.TyreAdded);
            
        }

        public IResult Delete(int id)
        {
            var tyre = _tyreDal.Get(tyre => tyre.Id == id);
            if (tyre != null)
                _tyreDal.Delete(tyre);

            return new SuccessResult(Messages.TyreDeleted);
        }
        public IDataResult<List<Tyre>> GetAll()
        {
            return new SuccessDataResult<List<Tyre>>(_tyreDal.GetAll(), Messages.TyreListed);
        }

        public IDataResult<Tyre> GetById(int id)
        {
            var tyre = _tyreDal.Get(tyre => tyre.Id == id);
            if (tyre == null)
                return new ErrorDataResult<Tyre>(Messages.TyreNotFound);
            else
                return new SuccessDataResult<Tyre>(tyre);
        }

        public IDataResult<Tyre> GetByNameForValue(string tyreName)
        {
            var tyreNameValue = _tyreDal.Get(tyreNameValue => tyreNameValue.TyreName == tyreName);
            if (tyreNameValue == null)
            {
                return new ErrorDataResult<Tyre>(Messages.TyreNotFound);
            }
            else
                return new SuccessDataResult<Tyre>(tyreNameValue);
        }

        public IDataResult<List<Tyre>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<Tyre>>(_tyreDal.GetForPageable(null, pageIndex, pageCount));
        }

        

        public Pageable<Tyre> Search(string tyreName, int id, int tyreCategoryId, int tyreBrandId, int pageIndex, int pageCount)
        {
            Expression<Func<Tyre, bool>> searhQuery = tyre =>
            (!string.IsNullOrWhiteSpace(tyreName) ? tyre.TyreName.Contains(tyreName) : true) &&
            (id > 0 ? tyre.Id == id : true) &&
            (tyreCategoryId > 0 ? tyre.TyreCategoryId == tyreCategoryId : true) &&
            (tyreBrandId > 0 ? tyre.TyreBrandId == tyreBrandId : true);

            var tyres = _tyreDal.GetForPageable(searhQuery, pageIndex, pageCount);
            var count = _tyreDal.GetCount(searhQuery);
            var data = new Pageable<Tyre>(pageIndex, pageCount, count, tyres);

            return data;

        }

        public IResult Update(int id, UpdateTyreRequest request)
        {
            var tyre = _tyreDal.Get(tyre => tyre.Id == id);
            if (tyre != null)
            {
                var tyreCategory = _tyreCategoryService.GetById(id);
                if (!tyreCategory.Success)
                    return new ErrorResult(tyreCategory.Message);

                var tyreBrand = _tyreBrandService.GetById(id);
                if (!tyreBrand.Success)
                    return new ErrorResult(tyreBrand.Message);

                tyre.TyreBrandId = request.TyreBrandId;
                tyre.TyreCategoryId = request.TyreCategoryId;
                tyre.Description = request.Description;
                tyre.TyreName = request.TyreName;

                _tyreDal.Update(tyre);
                return new SuccessResult(Messages.TyreUpdated);
            }
            return new ErrorResult(Messages.TyreNotUpdated);

        }
    }
}
