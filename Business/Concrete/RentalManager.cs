using System.Linq.Expressions;
using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Requests.Rentals;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        ICarDal _carDal;
        ICarService _carService;
        ICustomerService _customerService;

        private IRentalService _rentalServiceImplementation;

        public RentalManager(IRentalDal rentalDal, ICarDal carDal, ICarService carService, ICustomerService customerService)
        {
            _rentalDal = rentalDal;
            _carDal = carDal;
            _carService = carService;
            _customerService = customerService;

        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalsListed);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalsDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalsDetails());
        }

        //[ValidationAspect(typeof(RentalValidator))]
        public IResult Add(CreateRentalRequest request)
        {
            //ValidationTool.Validate(new RentalValidator(), rental);

            Rental rental = new Rental();

            var car = _carService.GetById(request.CarId);
            if (car.Success)
                rental.CarId = request.CarId;
            else
                return new ErrorResult(Messages.CarNotFound);

            var customerId = _customerService.GetById(request.CustomerId);
            if (customerId.Success)
                rental.CustomerId = request.CustomerId;
            else
                return new ErrorResult(Messages.NotFoundCustomer);
            rental.RentDate = request.RentDate;
            if (request.RentDate > request.ReturnDate)
                return new ErrorResult(Messages.RentDateCannotBeGreaterThanReturnDate);
            else
                rental.RentDate = request.RentDate;
            rental.ReturnDate = request.ReturnDate;

                _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        public IResult Update(int id, UpdateRentalRequest request)
        {
            var rental = _rentalDal.Get(rental => rental.Id == id);
            if (rental != null)
            {
                rental.CarId = request.CarId;
                rental.CustomerId = request.CustomerId;
                rental.RentDate = request.RentDate;
                rental.ReturnDate = request.ReturnDate;
                _rentalDal.Update(rental);
                return new SuccessResult(Messages.RentalUpdated);
            }
            else
                return new ErrorResult(Messages.RentalNotUpdated);
        }

        public IResult Delete(int id)
        {
            var rental = _rentalDal.Get(rental => rental.Id == id);
            if(rental != null)
                _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult IsCarAvaible(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId).Any();
            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        public List<int> CalculateTotalPrice(DateTime rentDate, DateTime returnDate, int carId)
        {
            List<int> totalAmount = new List<int>();
            var dateDifference = (returnDate - rentDate).Days;
            var dailyCarPrice = decimal.ToInt32(_carDal.Get(c => c.CarId == carId).DailyPrice);

            var totalPrice = dateDifference * dailyCarPrice;

            totalAmount.Add(dateDifference);
            totalAmount.Add(totalPrice);


            return totalAmount;
        }

        public IDataResult<List<Rental>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetForPageable(null, pageIndex, pageCount), Messages.RentalPaging);
        }

        public Core.Entities.Pageable<Rental> Search(int id, int carId, int customerId, DateTime rentDate, DateTime returnDate, int pageIndex, int pageCount)
        {
            Expression<Func<Rental, bool>> searchQuery = rental =>
            (rentDate == null ? rental.RentDate == rentDate : true) &&
            (returnDate == null ? rental.ReturnDate == returnDate : true) &&
            (id > 0 ? rental.Id == id : true) &&
            (carId > 0 ? rental.CarId == carId : true) &&
            (customerId > 0 ? rental.CustomerId == customerId : true);

            var rentals = _rentalDal.GetForPageable(searchQuery, pageIndex, pageCount);
            var count = _rentalDal.GetCount(searchQuery);
            var data = new Pageable<Rental>(pageIndex, pageCount, count, rentals);

            return data;


        }
    }
}