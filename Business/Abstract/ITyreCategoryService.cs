using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.TyreCategories;

namespace Business.Abstract
{
    public interface ITyreCategoryService
    {
        IDataResult<List<TyreCategory>> GetAll();
        IDataResult<List<TyreCategory>> GetForPageable(int pageIndex, int pageCount);
        Pageable<TyreCategory> Search(string tyreCategoryName, string tyreSpeedIndex, int id, int pageIndex, int pageCount);
        IDataResult<TyreCategory> GetById(int id);
        IResult Add(CreateTyreCategoryRequest request);
        IResult Delete(int id);
        IResult Update(int id, UpdateTyreCategoryRequest request);

    }
}
