using Core.Business;
using Core.Utilities.Results;
using Entities.Concrete;
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
        IResult Delete(CarImage carImage);
        IResult Update(IFormFile file, CarImage carImage);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetByImageId(int id); // Fotoğraf silerken kullanıyorum.
        IDataResult<List<CarImage>> GetForPageable(int pageIndex, int pageCount);
        IDataResult<List<CarImage>> Search(int id, int carId, int pageIndex, int pageCount);

    }
}