using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarsByBrandId(int brandId);
        List<CarDetailDto> GetCarsByColorId(int colorId);
        List<CarDetailDto> GetCarDetails();
        List<CarDetailDto> GetCarDetailsByColorAndByBrand(int colorId, int brandId);
        List<CarDetailDto> GetCarDetailsByCarId(int carId);
    }
}