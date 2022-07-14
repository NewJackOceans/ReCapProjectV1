using Business.Abstract;
using Entities.Requests.Tyres;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TyresController : ControllerBase
    {
        ITyreService _tyreService;

        public TyresController(ITyreService tyreService)
        {
            _tyreService = tyreService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int id, [FromQuery] int tyreCategoryId, [FromQuery] int tyreBrandId, [FromQuery] string tyreName = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _tyreService.Search(tyreName, id, tyreCategoryId, tyreBrandId, pageIndex, pageCount);

            return Ok(result);

        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateTyreRequest request)
        {
            var result = _tyreService.Add(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _tyreService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateTyreRequest request)
        {
            var result = _tyreService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
