using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.TyreCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TyreCategoriesController : ControllerBase
    {
        ITyreCategoryService _tyreCategoryService;

        public TyreCategoriesController(ITyreCategoryService tyreCategoryService)
        {
            _tyreCategoryService = tyreCategoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pageable<TyreCategory>), 200)]
        public IActionResult GetAll([FromQuery] int id, [FromQuery] string tyreSpeedIndex ="", [FromQuery] string tyreCategoryName = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _tyreCategoryService.Search(tyreCategoryName, tyreSpeedIndex, id, pageIndex, pageCount);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IResult), 200)]
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
        [ProducesResponseType(typeof(IResult), 200)]
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
        [ProducesResponseType(typeof(IResult), 200)]
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
