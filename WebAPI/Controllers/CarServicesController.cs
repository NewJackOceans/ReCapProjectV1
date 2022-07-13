using Business.Abstract;
using Entities.Requests.CarServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarServicesController : ControllerBase
    {
        private ICarServiceService _carServiceService;

        public CarServicesController(ICarServiceService carServiceService)
        {
            _carServiceService = carServiceService;
        }

        [HttpGet]
        public IActionResult Getall([FromQuery] int carId,  [FromQuery] DateTime serviceEntryDate, [FromQuery] DateTime serviceExitDate, [FromQuery] string serviceType = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _carServiceService.Search(carId, serviceType, serviceEntryDate, serviceExitDate, pageIndex, pageCount);

            return Ok(result);
        }

        [HttpPost]
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
