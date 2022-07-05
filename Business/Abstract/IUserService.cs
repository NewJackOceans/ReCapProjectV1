using Core.Entities.Concrete;
using Core.Entities.Requests.Users;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        IDataResult<List<User>> GetAll();
        IDataResult<List<User>> GetForPageable(int pageIndex, int pageCount);
        IDataResult<List<User>> Search(int id, bool status,string firstName, string lastName, string email, int pageIndex, int pageCount);
        IResult Add(User user);
        IResult Update(int id, UpdateUserRequest request);
        IResult Delete(int id);
        User GetByMail(string email);
    }
}
