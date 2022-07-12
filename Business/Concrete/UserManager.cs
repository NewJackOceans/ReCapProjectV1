using System.Linq.Expressions;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.Requests.Users;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private IUserService _userService;
        IAuthService _authService;

        public UserManager(IUserDal userDal, IUserService userService, IAuthService authService)
        {
            _userDal = userDal;
            _userService = userService;
            _authService = authService;
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
        public IResult Add(CreateUserRequest request)
        {
            //ValidationTool.Validate(new UserValidator(), user);
            User user = new User();

            var email = _authService.UserExists(request.EMail);
            if (email.Success)
                user.EMail = request.EMail;
            else
                return new ErrorResult(Messages.UserAlreadyExists);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;



            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Update(int id, UpdateUserRequest request)
        {
            var user = _userDal.Get(user => user.Id == id);
            if (user != null)
            {
                user.EMail = request.EMail;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Status = request.Status;
                _userDal.Update(user);
                return new SuccessResult(Messages.UserUpdated);
            }
            else
                return new ErrorResult(Messages.UserNotUpdated);
            
        }

        public IResult Delete(int id)
        {
            var user = _userDal.Get(user => user.Id == id);
            if(user != null)
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

        public IDataResult<List<User>> Search(int id, bool status, string firstName, string lastName, string email, int pageIndex, int pageCount)
        {
            Expression<Func<User, bool>> searchQuery = user =>
            (!string.IsNullOrWhiteSpace(firstName) ? user.FirstName.Contains(firstName) : true) &&
            (!string.IsNullOrWhiteSpace(lastName) ? user.LastName.Contains(lastName) : true) &&
            (!string.IsNullOrWhiteSpace(email) ? user.EMail.Contains(email) : true) &&
            (id > 0 ? user.Id == id : true) &&
            (status != null ? user.Status == status : true) ;
            return new SuccessDataResult<List<User>>(_userDal.GetForPageable(searchQuery, pageIndex, pageCount), Messages.UserPaging);
        }

        public IDataResult<User> GetById(int id)
        {
            var user = _userDal.Get(user => user.Id == id);
            if (user != null)
            {
                return new ErrorDataResult<User>(Messages.UserIsAvailable);
            }
            return new SuccessDataResult<User>(user);
        }

        public void Add(User user)
        {
            throw new NotImplementedException();
        }
    }
}