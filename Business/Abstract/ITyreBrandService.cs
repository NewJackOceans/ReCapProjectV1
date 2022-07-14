using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.TyreBrands;

namespace Business.Abstract
{
    public interface ITyreBrandService
    {
        IDataResult<List<TyreBrand>> GetAll();
        IDataResult<List<TyreBrand>> GetForPageable(int pageIndex, int pageCount);
        Pageable<TyreBrand> Search(int id, string tyreBrandName, int pageIndex, int pageCount);
        IDataResult<TyreBrand> GetById(int id);
        IResult Add(CreateTyreBrandRequest request);
        IResult Delete(int id);
        IResult Update(int id, UpdateTyreBrandRequest request);
    }
}
