using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Requests.Rentals;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<RentalDetailDto>> GetRentalsDetails();
        IDataResult<List<Rental>> GetForPageable(int pageIndex, int pageCount);
        Pageable<Rental> Search(int id, int carId, int customerId, DateTime rentDate, DateTime returnDate, int pageIndex, int pageCount);
        IResult Add(CreateRentalRequest request);
        IResult Update(int id, UpdateRentalRequest request);
        IResult Delete(int id);
        IResult IsCarAvaible(int carId);
        List<int> CalculateTotalPrice(DateTime rentDate, DateTime returnDate, int carId);
    }
}
