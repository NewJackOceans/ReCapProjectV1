using System.Linq.Expressions;
using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Entities.Requests.Users;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTOs;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
        }

        //[ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {            
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Update(int id, UserForRegisterDto userForRegisterDto)
        {
            var user = _userDal.Get(user => user.Id == id);
            if (user != null)
            {
                user.EMail = userForRegisterDto.Email;
                user.FirstName = userForRegisterDto.FirstName;
                user.LastName = userForRegisterDto.LastName;
                _userDal.Update(user);
                return new SuccessResult(Messages.UserUpdated);
            }
            else
                return new ErrorResult(Messages.UserNotUpdated);

        }

        public IResult Delete(int id)
        {
            var user = _userDal.Get(user => user.Id == id);
            if (user != null)
                _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }
        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.EMail == email);
        }

        public IDataResult<List<User>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<User>>(_userDal.GetForPageable(null, pageIndex, pageCount), Messages.UserPaging);
        }

        public Pageable<User> Search(int id, bool status, string firstName, string lastName, string email, int pageIndex, int pageCount)
        {
            Expression<Func<User, bool>> searchQuery = user =>
            (!string.IsNullOrWhiteSpace(firstName) ? user.FirstName.Contains(firstName) : true) &&
            (!string.IsNullOrWhiteSpace(lastName) ? user.LastName.Contains(lastName) : true) &&
            (!string.IsNullOrWhiteSpace(email) ? user.EMail.Contains(email) : true) &&
            (id > 0 ? user.Id == id : true) &&
            (status != null ? user.Status == status : true);

            var users = _userDal.GetForPageable(searchQuery, pageIndex, pageCount);
            var count = _userDal.GetCount(searchQuery);
            var data = new Pageable<User>(pageIndex, pageCount, count, users);

            return data;


        }

        public IDataResult<User> GetById(int id)
        {
            var user = _userDal.Get(user => user.Id == id);
            if (user == null)
            {
                return new ErrorDataResult<User>(Messages.UserIsAvailable);
            }
            return new SuccessDataResult<User>(user);
        }

    }
}