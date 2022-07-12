using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;
using Entities.Requests.Brands;
using Core.Entities;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IDataResult<List<Brand>> GetAll()
        {
            
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandsListed);
        }

        //[ValidationAspect(typeof(BrandValidator))]
        public IResult Add(CreateBrandRequest request)
        {
            //ValidationTool.Validate(new BrandValidator(), brand);

            Brand brand = new Brand();
            
            var brandName = _brandDal.Get(brand => brand.BrandName == request.BrandName);
            if (brandName == null)
                brand.BrandName = request.BrandName;
            else
                return new ErrorResult(Messages.BrandNameIsAvailable);

            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdded);


        }

        public IResult Update(int id, UpdateBrandRequest request)
        {
            var brand = _brandDal.Get(brand => brand.BrandId == id);
            if (brand != null)
            {   

                brand.BrandName = request.BrandName;
                _brandDal.Update(brand);
                return new SuccessResult(Messages.BrandUpdated);
            }
            else            
                return new ErrorResult(Messages.BrandNotUpdated);
            
        }

        public IResult Delete(int id)
        {
            var brand = _brandDal.Get(brand => brand.BrandId == id);
            if (brand != null)
                _brandDal.Delete(brand);

            return new SuccessResult(Messages.BrandDeleted);
        }

        public IDataResult<List<Brand>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetForPageable(null, pageIndex, pageCount), Messages.BrandPaging);
        }

        public Pageable<Brand> Search(string brandName, int brandId, int pageIndex, int pageCount)
        {
            Expression<Func<Brand, bool>> searchQuery = brand =>
            (!string.IsNullOrWhiteSpace(brandName) ? brand.BrandName.Contains(brandName) : true) &&
            (brandId > 0 ? brand.BrandId == brandId : true);

            var brands = _brandDal.GetForPageable(searchQuery, pageIndex, pageCount);
            var count = _brandDal.GetCount(searchQuery);
            var data = new Pageable<Brand>(pageIndex, pageCount, count, brands);

            return data;
        }

        

        public IDataResult<Brand> GetById(int id)
        {
            var brand = _brandDal.Get(brand => brand.BrandId == id);
            if (brand == null)
            {
                return new ErrorDataResult<Brand>(Messages.NotFoundBrand);
            }
            return new SuccessDataResult<Brand>(brand);
        }

        public IDataResult<Brand> GetByName(string name)
        {
            var brand = _brandDal.Get(brand => brand.BrandName == name);
            if (brand != null)
            {
                return new ErrorDataResult<Brand>(Messages.BrandNameIsAvailable);
            }
            return new SuccessDataResult<Brand>(brand);
        }

        public IResult CheckBrandId(int id)
        {
            if (_brandDal.Get(brand => brand.BrandId == id) == null)
            {
                throw new Exception(Messages.BrandIdNotAvailable);
            }
            return new SuccessResult();

        }
    }
}