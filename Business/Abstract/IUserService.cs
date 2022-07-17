using Core.Entities;
using Core.Entities.Concrete;
using Core.Entities.Requests.Users;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        IResult Add(User user);
        User GetByMail(string email);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
        Pageable<User> Search(int id, bool status,string firstName, string lastName, string email, int pageIndex, int pageCount);
        IResult Update(int id, UserForRegisterDto userForRegisterDto);
        IResult Delete(int id);
    }
}
