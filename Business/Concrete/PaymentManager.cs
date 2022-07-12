using System.Linq.Expressions;
using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Requests.Payments;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentDal;
        ICustomerService _customerService;

        public PaymentManager(IPaymentDal paymentDal, ICustomerService customerService)
        {
            _paymentDal = paymentDal;
            _customerService = customerService;            
        }

        public IResult Add(CreatePaymentRequest request)
        {
            Payment payment = new Payment();

            var customerId = _customerService.GetById(request.CustomerId);
            if (customerId.Success)
                payment.CustomerId = request.CustomerId;
            else
                return new ErrorResult(Messages.NotFoundCustomer);
            


            payment.CreditCardNumber = request.CreditCardNumber;
            payment.Price = request.Price;
            payment.ExpirationDate = request.ExpirationDate;
            payment.SecurityCode = request.SecurityCode;


            _paymentDal.Add(payment);
            return new SuccessResult("Payment added");

        }

        public IResult Delete(int id)
        {
            var payment = _paymentDal.Get(payment => payment.PaymentId == id);
            if(payment != null)
                _paymentDal.Delete(payment);
            return new SuccessResult("Payment deleted");
        }

        public IResult Update(int id, UpdatePaymentRequest request)
        {
            var payment = _paymentDal.Get(payment => payment.PaymentId ==id);
            if (payment != null)
            {
                payment.CreditCardNumber = request.CreditCardNumber;
                payment.Price = request.Price;
                payment.CustomerId = request.CustomerId;
                payment.ExpirationDate = request.ExpirationDate;
                payment.SecurityCode = request.SecurityCode;
                _paymentDal.Update(payment);
                return new SuccessResult(Messages.PaymentUpdated);
            }
            else
                return new ErrorResult(Messages.PaymentNotUpdated);
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        public IDataResult<List<Payment>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetForPageable(null, pageIndex, pageCount), Messages.PaymentPaging);
        }

        public Pageable<Payment> Search(int paymentId, int customerId, int pageIndex, int pageCount)
        {
            Expression<Func<Payment, bool>> searchQuery = payment =>
            (paymentId > 0 ? payment.PaymentId == paymentId : true) &&
            (customerId > 0 ? payment.CustomerId == customerId : true);

            var payments = _paymentDal.GetForPageable(searchQuery, pageIndex, pageCount);
            var count = _paymentDal.GetCount(searchQuery);
            var data = new Pageable<Payment>(pageIndex, pageCount, count, payments);

            return data;
        }
    }
}