namespace Entities.Requests.Rentals
{
    public class UpdateRentalRequest
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
