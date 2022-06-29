using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<Rental>> GetRentalById(int rentalId);
        IDataResult<List<RentalDetailDto>> GetRentalsDetails();
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
        IResult IsCarAvaible(int carId);
        List<int> CalculateTotalPrice(DateTime rentDate, DateTime returnDate, int carId);
    }
}
