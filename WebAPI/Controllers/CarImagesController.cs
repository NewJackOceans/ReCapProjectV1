using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Core.Entities;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;
        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(Core.Utilities.Results.IResult), 200)]
        public IActionResult Add([FromForm] IFormFile file, [FromForm] CarImage carImage)
        {
            var result = _carImageService.Add(file, carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Core.Utilities.Results.IResult), 200)]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _carImageService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Core.Utilities.Results.IResult), 200)]
        public IActionResult Update([FromRoute]int id, [FromForm] IFormFile file)
        {
            var result = _carImageService.Update(id, file);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        [ProducesResponseType(typeof(Pageable<CarImage>), 200)]
        public IActionResult GetAll([FromQuery] int id, [FromQuery] int carId, [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _carImageService.Search(id, carId, pageIndex, pageCount);
            
                return Ok(result);
        }
        
    }
}