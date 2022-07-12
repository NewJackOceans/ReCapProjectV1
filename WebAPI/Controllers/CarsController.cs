using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Requests.Cars;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int carId, [FromQuery] int colorId,[FromQuery] int brandId, [FromQuery] string modelYear = "", string carName = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {

            var result = _carService.Search(carName, modelYear, carId, colorId, brandId, pageIndex, pageCount);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCarDetailsByCarId([FromRoute] int id)
        {
            var result = _carService.GetCarDetailsByCarId(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("details")]
        public IActionResult GetCarDetails()
        {
            var result = _carService.GetCarDetails();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateCarRequest request)
        {
            var result = _carService.Add(request);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _carService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateCarRequest request)
        {
            var result = _carService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}