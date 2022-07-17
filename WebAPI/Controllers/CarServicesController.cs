using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.CarServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarServicesController : ControllerBase
    {
        private ICarServiceService _carServiceService;

        public CarServicesController(ICarServiceService carServiceService)
        {
            _carServiceService = carServiceService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pageable<CarService>), 200)]
        public IActionResult Getall([FromQuery] int carId,  [FromQuery] DateTime serviceEntryDate, [FromQuery] DateTime serviceExitDate, [FromQuery] string serviceType = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _carServiceService.Search(carId, serviceType, serviceEntryDate, serviceExitDate, pageIndex, pageCount);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Add([FromBody] CreateCarServiceRequest request)
        {
            var result = _carServiceService.Add(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("id")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _carServiceService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("id")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Update([FromRoute] int id, UpdateCarServiceRequest request)
        {
            var result = _carServiceService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
