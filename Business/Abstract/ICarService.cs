using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Requests.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(CreateCarRequest request);
        IResult Update(int id, UpdateCarRequest request);
        IResult Delete(int id);
        IDataResult<List<Car>> GetAll();
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<List<CarDetailDto>> GetCarDetailsByCarId(int carId);
        IResult AddTransactionalTest(Car car);
        IDataResult<List<Car>> GetForPageable(int pageIndex, int pageCount);
        IDataResult<List<Car>> Search(string carName, string modelYear, int carId, int colorId, int brandId, int pageIndex, int pageCount);

    }
}
