using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(IFormFile file, CarImage carImage);
        IResult Delete(int id);
        IResult Update(int id, IFormFile file);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetByImageId(int id); // Fotoğraf silerken kullanıyorum.
        IDataResult<List<CarImage>> GetForPageable(int pageIndex, int pageCount);
        Pageable<CarImage> Search(int id, int carId, int pageIndex, int pageCount);

    }
}