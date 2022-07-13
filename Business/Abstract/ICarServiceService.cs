using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.CarServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarServiceService
    {
        IResult Add(CreateCarServiceRequest request);
        IResult Update(int id, UpdateCarServiceRequest request);
        IResult Delete(int id);
        IDataResult<List<CarService>> GetAll();
        IDataResult<CarService> GetById(int id);
        IDataResult<List<CarService>> GetForPageable(int pageIndex, int pageCount);
        Pageable<CarService> Search(int carId, string serviceType, DateTime serviceEntryDate, DateTime serviceExitDate, int pageIndex, int pageCount);



    }
}
