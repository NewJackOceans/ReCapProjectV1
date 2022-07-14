using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.CarTyreChanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarTyreChangeService
    {
        IDataResult<List<CarTyreChange>> GetAll();
        IDataResult<List<CarTyreChange>> GetForPageable(int pageIndex, int pageCount);
        Pageable<CarTyreChange> Search(int id, int carId, int tyreId, int tyreChangeKm, DateTime tyreChangeDate, int pageIndex, int pageCount);
        IResult Add(CreateCarTyreChangeRequest request);
        IResult Delete(int id);
        IResult Update(int id, UpdateCarTyreChangeRequest request);
    }
}
