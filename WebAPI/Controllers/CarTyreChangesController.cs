using Business.Abstract;
using Entities.Requests.CarTyreChanges;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarTyreChangesController : ControllerBase
    {
        ICarTyreChangeService _carTyreChangeService;

        public CarTyreChangesController(ICarTyreChangeService carTyreChangeService)
        {
            _carTyreChangeService = carTyreChangeService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int id, [FromQuery] int carId, [FromQuery] int tyreBrandId, [FromQuery] int tyreChangeKm, [FromQuery] DateTime tyreChangeDate, [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _carTyreChangeService.Search(id, carId, tyreBrandId, tyreChangeKm, tyreChangeDate, pageIndex, pageCount);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateCarTyreChangeRequest request)
        {
            var result = _carTyreChangeService.Add(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _carTyreChangeService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateCarTyreChangeRequest request)
        {
            var result = _carTyreChangeService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
