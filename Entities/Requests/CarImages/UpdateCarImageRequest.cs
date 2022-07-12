

namespace Entities.Requests.CarImages
{
    public class UpdateCarImageRequest
    {
        public int CarId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
    }
}
