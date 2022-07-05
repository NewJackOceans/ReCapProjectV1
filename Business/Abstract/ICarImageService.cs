using Core.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.CarImages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        IDataResult<List<CarImage>> Search(int id, int carId, int pageIndex, int pageCount);

    }
}