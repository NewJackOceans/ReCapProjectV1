using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Requests.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBrandService
    {
        IDataResult<List<Brand>> GetAll();
        IDataResult<List<Brand>> GetForPageable(int pageIndex, int pageCount);
        IDataResult<List<Brand>> Search(string brandName, int brandId, int pageIndex, int pageCount);
        IResult Add(CreateBrandRequest request);
        IResult Update(int id, UpdateBrandRequest request);
        IResult Delete(int id);
        IDataResult<Brand> GetById(int id);
        IDataResult<Brand> GetByName(string name);
        IResult CheckBrandId(int id);
    }
}
