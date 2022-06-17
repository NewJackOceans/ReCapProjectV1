using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
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
    public class BrandManager : IBrandService
    {
           
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Brand brand)
        {
            if (brand.Name.Length < 2)
            {
                return new ErrorResult(Messages.BrandNameInValid);

            }else
            {
                return new ErrorResult(Messages.BrandAdded);
            }
        }

        public IDataResult<List<Brand>> GetAll()
        {
            if (DateTime.Now.Hour == 3)
            {
                return new ErrorDataResult<List<Brand>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.BrandListed);
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == brandId));
        }
    }
}