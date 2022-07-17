using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.CarTyreChanges;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarTyreChangesController : ControllerBase
    {
        ICarTyreChangeService _carTyreChangeService;

        public CarTyreChangesController(ICarTyreChangeService carTyreChangeService)
        {
            _carTyreChangeService = carTyreChangeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pageable<CarTyreChange>), 200)]
        public IActionResult GetAll([FromQuery] int id, [FromQuery] int carId, [FromQuery] int tyreId, [FromQuery] int tyreChangeKm, [FromQuery] DateTime tyreChangeDate, [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _carTyreChangeService.Search(id, carId, tyreId, tyreChangeKm, tyreChangeDate, pageIndex, pageCount);

            return Ok(result);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(IResult), 200)]
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
        [ProducesResponseType(typeof(IResult), 200)]
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
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateCarTyreChangeRequest request)
        {
            var result = _carTyreChangeService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("bulk")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult AddBulk([FromBody] CreateBulkCarTyreChangeRequest request)
        {
            var result = _carTyreChangeService.AddBulk(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("bulkname")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult AddBulkForName([FromBody] CreateBulkNameCarTyreChangeRequest request)
        {
            var result = _carTyreChangeService.AddBulkForName(request);
            

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
