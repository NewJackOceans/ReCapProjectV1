using Business.Abstract;
using Entities.Requests.TyreCategories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TyreCategoriesController : ControllerBase
    {
        ITyreCategoryService _tyreCategoryService;

        public TyreCategoriesController(ITyreCategoryService tyreCategoryService)
        {
            _tyreCategoryService = tyreCategoryService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int id, [FromQuery] string tyreSpeedIndex ="", [FromQuery] string tyreCategoryName = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _tyreCategoryService.Search(tyreCategoryName, tyreSpeedIndex, id, pageIndex, pageCount);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateTyreCategoryRequest request)
        {
            var result = _tyreCategoryService.Add(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _tyreCategoryService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateTyreCategoryRequest request)
        {
            var result = _tyreCategoryService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
