using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.TyreBrands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TyreBrandsController : ControllerBase
    {
        ITyreBrandService _tyreBrandService;

        public TyreBrandsController(ITyreBrandService tyreBrandService)
        {
            _tyreBrandService = tyreBrandService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pageable<TyreBrand>), 200)]
        public IActionResult GetAll([FromQuery] int id, [FromQuery] string tyreBrandName = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _tyreBrandService.Search(id, tyreBrandName, pageIndex, pageCount);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Add([FromBody] CreateTyreBrandRequest request)
        {
            var result = _tyreBrandService.Add(request);
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
            var result = _tyreBrandService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateTyreBrandRequest request)
        {
            var result = _tyreBrandService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
