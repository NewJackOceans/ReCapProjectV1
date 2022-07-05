using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.Payments;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IResult Add(Payment payment);
        IResult Delete(int id);
        IResult Update(int id, UpdatePaymentRequest request);

        IDataResult<List<Payment>> GetAll();
        IDataResult<List<Payment>> GetForPageable(int pageIndex, int pageCount);
        IDataResult<List<Payment>> Search(int paymentId, int customerId, int pageIndex, int pageCount);
    }
}