using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Entities.Concrete;
using Core.Entities.Requests.Users;
using Core.Utilities.Results;
using DataAccess.Abstract;

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

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            ValidationTool.Validate(new UserValidator(), user);

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
    }
}