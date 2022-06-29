using Core.Entities.Concrete;
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
        IDataResult<List<User>> GetUserById(int id);
        User GetByMail(string email);
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
    }
}
