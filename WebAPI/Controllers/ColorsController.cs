using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Requests.Colors;
using Microsoft.AspNetCore.Authorization;
using Core.Entities;
using Entities.Concrete;
using Core.Utilities.Results;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ColorsController : ControllerBase
    {
        private IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pageable<Color>), 200)]
        public IActionResult GetAll([FromQuery] int colorId, [FromQuery] string colorName = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _colorService.Search(colorName, colorId, pageIndex, pageCount);
            
                return Ok(result);
        }      

        [HttpPost]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Add([FromBody] CreateColorRequest request)
        {
            var result = _colorService.Add(request);
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
            var result = _colorService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateColorRequest request)
        {
            var result = _colorService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}