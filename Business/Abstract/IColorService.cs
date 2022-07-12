using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.Colors;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<List<Color>> GetAll();
        IDataResult<Color> GetById(int id);
        IDataResult<List<Color>> GetForPageable(int pageIndex, int pageCount);
        IDataResult<List<Color>> Search(string colorName, int colorId, int pageIndex, int pageCount);
        IResult Add(CreateColorRequest request);
        IResult Update(int id, UpdateColorRequest request);
        IResult Delete(int id);
    }
}
