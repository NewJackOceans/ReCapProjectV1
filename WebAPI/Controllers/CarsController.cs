using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Requests.Cars;
using Microsoft.AspNetCore.Authorization;
using Core.Entities;
using Entities.Concrete;
using Core.Utilities.Results;
using System.Collections.Generic;
using Entities.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pageable<Car>),200)]
        public IActionResult GetAll([FromQuery] int carId, [FromQuery] int colorId,[FromQuery] int brandId, [FromQuery] string modelYear = "", [FromQuery] string carName = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {

            var result = _carService.Search(carName, modelYear, carId, colorId, brandId, pageIndex, pageCount);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IDataResult<List<CarDetailDto>>), 200)]

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
        [ProducesResponseType(typeof(IDataResult<List<CarDetailDto>>), 200)]
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
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Add([FromBody] CreateCarRequest request)
        {
            var result = _carService.Add(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IResult), 200)]
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
        [ProducesResponseType(typeof(IResult), 200)]
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