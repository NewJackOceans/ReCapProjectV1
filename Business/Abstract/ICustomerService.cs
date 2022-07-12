using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.Customers;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> GetById(int id);
        IDataResult<List<Customer>> GetForPageable(int pageIndex, int pageCount);
        Pageable<Customer> Search(string companyName, int customerId, int userId,  int pageIndex, int pageCount);
        IResult Add(CreateCustomerRequest request);
        IResult Update(int id, UpdateCustomerRequest request);
        IResult Delete(int id);
    }
}
