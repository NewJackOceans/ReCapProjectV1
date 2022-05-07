using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Salesforce.Common.Models.Json;
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

        public object Messages { get; private set; }

        public void Add(Brand brand)
        {
            if (brand.Name.Length > 2)
            {
                _brandDal.Add(brand);
                return new(Messages.BrandAdded);

            }
            else
            {
                return new (Messages.BrandNameInValid);
            }
        }

        public List<Brand> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Brand> GetById(int brandId)
        {
            throw new NotImplementedException();
        } 
        

        
    }
}