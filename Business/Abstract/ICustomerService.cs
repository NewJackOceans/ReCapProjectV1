using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> GetById(int id);
        IDataResult<List<Customer>> GetForPageable(int pageIndex, int pageCount);
        IDataResult<List<Customer>> Search(string companyName, int customerId, int userId,  int pageIndex, int pageCount);
        IResult Add(CreateCustomerRequest request);
        IResult Update(int id, UpdateCustomerRequest request);
        IResult Delete(int id);
    }
}
