using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Requests.TyreCategories;
using HT.Core;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class TyreCategoryManager : ITyreCategoryService
    {
        ITyreCategoryDal _tyreCategoryDal;

        public TyreCategoryManager(ITyreCategoryDal tyreCategoryDal)
        {
            _tyreCategoryDal = tyreCategoryDal;
        }

        public IResult Add(CreateTyreCategoryRequest request)
        {
            TyreCategory tyreCategory = new TyreCategory();

            tyreCategory.TyreCategoryName = request.TyreCategoryName;
            tyreCategory.TyreSpeedIndex = request.TyreSpeedIndex;

            _tyreCategoryDal.Add(tyreCategory);
            return new SuccessResult(Messages.TyreCategoryAdded);
        }

        public IResult Delete(int id)
        {
            var tyreCategory = _tyreCategoryDal.Get(tyreCategory => tyreCategory.Id == id);
            if (tyreCategory != null)
                _tyreCategoryDal.Delete(tyreCategory);
            return new SuccessResult(Messages.TyreCategoryDeleted);
        }

        public IDataResult<List<TyreCategory>> GetAll()
        {
            return new SuccessDataResult<List<TyreCategory>>(_tyreCategoryDal.GetAll(), Messages.TyreCategoryListed);
        }

        public IDataResult<TyreCategory> GetById(int id)
        {
            var tyreCategory = _tyreCategoryDal.Get(tyreCategory => tyreCategory.Id == id);
            if (tyreCategory == null)
                return new ErrorDataResult<TyreCategory>(Messages.TyreCategoryListed);
            else
                return new SuccessDataResult<TyreCategory>(tyreCategory);
        }

        public IDataResult<List<TyreCategory>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<TyreCategory>>(_tyreCategoryDal.GetForPageable(null, pageIndex, pageCount));
        }

        public Pageable<TyreCategory> Search(string tyreCategoryName, string tyreSpeedIndex, int id, int pageIndex, int pageCount)
        {
            Expression<Func<TyreCategory, bool>> searcQuery = tyreCategory =>
            (!string.IsNullOrWhiteSpace(tyreCategoryName) ? tyreCategory.TyreCategoryName.Contains(tyreCategoryName) : true) &&
            (!string.IsNullOrWhiteSpace(tyreSpeedIndex) ? tyreCategory.TyreSpeedIndex.Contains(tyreSpeedIndex) : true) &&
            (id > 0 ? tyreCategory.Id == id : true);

            var tyreCategories = _tyreCategoryDal.GetForPageable(searcQuery, pageIndex, pageCount);
            var count = _tyreCategoryDal.GetCount(searcQuery);
            var data = new Pageable<TyreCategory>(pageIndex, pageCount, count, tyreCategories);

            return data;
        }

        public IResult Update(int id, UpdateTyreCategoryRequest request)
        {
            var tyreCategory = _tyreCategoryDal.Get(tyreCategory => tyreCategory.Id == id);
            if (tyreCategory != null)
            {
                tyreCategory.TyreCategoryName = request.TyreCategoryName;
                tyreCategory.TyreSpeedIndex = request.TyreSpeedIndex;

                _tyreCategoryDal.Update(tyreCategory);
                return new SuccessResult(Messages.TyreCategoryUpdated);
            }
            return new ErrorResult(Messages.TyreCategoryNotUpdated);
        }
    }
}
