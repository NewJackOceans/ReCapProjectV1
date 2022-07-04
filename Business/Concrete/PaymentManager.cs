using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentDal;

        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        public IResult Add(Payment payment)
        {
            _paymentDal.Add(payment);
            return new SuccessResult("Payment added");

        }

        public IResult Delete(Payment payment)
        {
            _paymentDal.Delete(payment);
            return new SuccessResult("Payment deleted");
        }

        public IResult Update(Payment payment)
        {
            _paymentDal.Update(payment);
            return new SuccessResult("payment updated");
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        public IDataResult<List<Payment>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetForPageable(null, pageIndex, pageCount), Messages.PaymentPaging);
        }

        public IDataResult<List<Payment>> Search(int paymentId, int customerId, int pageIndex, int pageCount)
        {
            Expression<Func<Payment, bool>> searchQuery = payment =>
            (paymentId > 0 ? payment.PaymentId == paymentId : true) &&
            (customerId > 0 ? payment.CustomerId == customerId : true);
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetForPageable(searchQuery, pageIndex, pageCount), Messages.PaymentPaging);
        }
    }
}