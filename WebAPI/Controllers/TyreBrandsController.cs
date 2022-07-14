using Business.Abstract;
using Entities.Requests.TyreBrands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TyreBrandsController : ControllerBase
    {
        ITyreBrandService _tyreBrandService;

        public TyreBrandsController(ITyreBrandService tyreBrandService)
        {
            _tyreBrandService = tyreBrandService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int id, [FromQuery] string tyreBrandName = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _tyreBrandService.Search(id, tyreBrandName, pageIndex, pageCount);
            return Ok(result);
        }

        [HttpPost]
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
