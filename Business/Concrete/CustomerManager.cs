using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Requests.Customers;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _costumerDal;
        IUserService _userService;
        

        public CustomerManager(ICustomerDal costumerDal, IUserService userService)
        {
            _costumerDal = costumerDal;
            _userService = userService;
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_costumerDal.GetAll(), Messages.CustomersListed);
        }

        //[ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(CreateCustomerRequest request)
        {
            //ValidationTool.Validate(new CustomerValidator(), customer);

            Customer customer = new Customer();

            var user = _costumerDal.Get(userId => userId.UserId == request.UserId);
            if (user != null)
                return new ErrorResult(Messages.UserIsAvailable);
            else
                customer.UserId = request.UserId;
                

            var companyName = _costumerDal.Get(company => company.CompanyName == request.CompanyName);
            if (companyName != null)
                return new ErrorResult(Messages.CompanyNameIsAvailable);
            else
                customer.CompanyName = request.CompanyName;
            

            _costumerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }

        public IResult Update(int id, UpdateCustomerRequest request)
        {
            var customer = _costumerDal.Get(customer => customer.CustomerId == id);
            if (customer != null)
            {
                customer.UserId = request.UserId;
                customer.CompanyName = request.CompanyName;
                _costumerDal.Update(customer);
                return new SuccessResult(Messages.CustomerUpdated);
            }
            else
                return new ErrorResult(Messages.CustomerNotUpdated);
        }

        public IResult Delete(int id)
        {
            var customer = _costumerDal.Get(customer => customer.UserId == id);
            if(customer != null)
                _costumerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        public IDataResult<List<Customer>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<Customer>>(_costumerDal.GetForPageable(null, pageIndex, pageCount), Messages.CustomerPaging);
        }

        public IDataResult<List<Customer>> Search(string companyName, int customerId, int userId, int pageIndex, int pageCount)
        {
            Expression<Func<Customer, bool>> searchQuery = customer =>
            (!string.IsNullOrWhiteSpace(companyName) ? customer.CompanyName.Contains(companyName) : true) &&
            (customerId > 0 ? customer.CustomerId == customerId : true) &&
            (userId > 0 ? customer.UserId == userId : true);
            return new SuccessDataResult<List<Customer>>(_costumerDal.GetForPageable(searchQuery, pageIndex, pageCount), Messages.CustomerPaging);
        }

        public IDataResult<Customer> GetById(int id)
        {
            var customer = _costumerDal.Get(customer => customer.CustomerId == id);
            if (customer == null)
            {
                return new ErrorDataResult<Customer>(Messages.NotFoundCustomer);
            }
            return new SuccessDataResult<Customer>(customer);
        }
    }
}