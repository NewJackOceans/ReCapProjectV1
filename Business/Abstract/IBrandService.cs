using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.Brands;

namespace Business.Abstract
{
    public interface IBrandService
    {
        IDataResult<List<Brand>> GetAll();
        IDataResult<List<Brand>> GetForPageable(int pageIndex, int pageCount);
        Pageable<Brand> Search(string brandName, int brandId, int pageIndex, int pageCount);
        IResult Add(CreateBrandRequest request);
        IResult Update(int id, UpdateBrandRequest request);
        IResult Delete(int id);
        IDataResult<Brand> GetById(int id);
        IDataResult<Brand> GetByName(string name);
        IResult CheckBrandId(int id);
    }
}
