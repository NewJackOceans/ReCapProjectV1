using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Requests.TyreBrands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TyreBrandManager : ITyreBrandService
    {
        ITyreBrandDal _tyreBrandDal;

        public TyreBrandManager(ITyreBrandDal tyreBrandDal)
        {
            _tyreBrandDal = tyreBrandDal;
        }

        public IResult Add(CreateTyreBrandRequest request)
        {
            TyreBrand tyreBrand = new TyreBrand();

            tyreBrand.TyreBrandName = request.TyreBrandName;

            _tyreBrandDal.Add(tyreBrand);
            return new SuccessResult(Messages.TyreBrandAdded);
        }

        public IResult Delete(int id)
        {
            var tyreBrand = _tyreBrandDal.Get(tyreBrand => tyreBrand.Id == id);
            if (tyreBrand != null)
                _tyreBrandDal.Delete(tyreBrand);

            return new SuccessResult(Messages.TyreBrandDeleted);
        }

        public IDataResult<List<TyreBrand>> GetAll()
        {
            return new SuccessDataResult<List<TyreBrand>>(_tyreBrandDal.GetAll(), Messages.TyreBrandListed);
        }

        public IDataResult<TyreBrand> GetById(int id)
        {
            var tyreBrand = _tyreBrandDal.Get(tyreBrand => tyreBrand.Id == id);
            if (tyreBrand == null)
                return new ErrorDataResult<TyreBrand>(Messages.TyreBrandNotFound);
            else
                return new SuccessDataResult<TyreBrand>(tyreBrand);
        }

        public IDataResult<List<TyreBrand>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<TyreBrand>>(_tyreBrandDal.GetForPageable(null, pageIndex, pageCount));
        }

        public Pageable<TyreBrand> Search(int id, string tyreBrandName, int pageIndex, int pageCount)
        {
            Expression<Func<TyreBrand, bool>> searchQuery = tyreBrand =>
            (!string.IsNullOrWhiteSpace(tyreBrandName) ? tyreBrand.TyreBrandName.Contains(tyreBrandName) : true) &&
            (id > 0 ? tyreBrand.Id == id : true);

            var tyreBrands = _tyreBrandDal.GetForPageable(searchQuery, pageIndex, pageCount);
            var count = _tyreBrandDal.GetCount(searchQuery);
            var data = new Pageable<TyreBrand>(pageIndex, pageCount, count, tyreBrands);

            return data;
        }

        public IResult Update(int id, UpdateTyreBrandRequest request)
        {
            var tyreBrand = _tyreBrandDal.Get(tyreBrand => tyreBrand.Id == id);
            if (tyreBrand != null)
            {
                tyreBrand.TyreBrandName = request.TyreBrandName;

                _tyreBrandDal.Update(tyreBrand);
                return new SuccessResult(Messages.TyreBrandUpdated);
            }
            return new ErrorResult(Messages.TyreBrandNotUpdated);
        }
    }
}
