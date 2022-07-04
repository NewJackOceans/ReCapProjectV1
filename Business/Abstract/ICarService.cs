using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<List<Car>> GetAll();
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<List<CarDetailDto>> GetCarDetailsByCarId(int carId);
        IResult AddTransactionalTest(Car car);
        IDataResult<List<Car>> GetForPageable(int pageIndex, int pageCount);
        IDataResult<List<Car>> Search(string modelYear, int carId, int colorId, int brandId, int pageIndex, int pageCount);
    }
}
