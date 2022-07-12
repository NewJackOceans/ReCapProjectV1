using Core.Entities;
using Core.Entities.Concrete;
using Core.Entities.Requests.Users;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
        IDataResult<List<User>> GetForPageable(int pageIndex, int pageCount);
        Pageable<User> Search(int id, bool status,string firstName, string lastName, string email, int pageIndex, int pageCount);
        IResult Add(CreateUserRequest request);
        IResult Update(int id, UpdateUserRequest request);
        IResult Delete(int id);
        User GetByMail(string email);
        void Add(User user);
    }
}
