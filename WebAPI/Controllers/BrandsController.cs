using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Requests.Brands;
using Microsoft.AspNetCore.Authorization;
using Core.Entities;
using Entities.Concrete;
using Core.Utilities.Results;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandsController : ControllerBase
    {
        private IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pageable<Brand>), 200)]
        public IActionResult GetAll([FromQuery] int brandId, [FromQuery] string brandName = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            var result = _brandService.Search(brandName, brandId, pageIndex, pageCount);
            
                return Ok(result);
            
        }

        [HttpPost]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Add([FromBody] CreateBrandRequest request)
        {
            var result = _brandService.Add(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateBrandRequest request)
        {
            var result = _brandService.Update(id, request);
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

            var result = _brandService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}