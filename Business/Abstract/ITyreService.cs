using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.Tyres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITyreService
    {
        IDataResult<List<Tyre>> GetAll();
        IDataResult<List<Tyre>> GetForPageable(int pageIndex, int pageCount);
        Pageable<Tyre> Search(string tyreName, int id, int tyreCategoryId, int tyreBrandId, int pageIndex, int pageCount);
        IResult Add(CreateTyreRequest request);
        IDataResult<Tyre> GetById(int id);
        IResult Delete(int id);
        IResult Update(int id, UpdateTyreRequest request);
        IDataResult<Tyre> GetByNameForValue(string tyreName);



    }
}
