using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Business.Abstract;
using Entities.Requests.Customers;
using Microsoft.AspNetCore.Authorization;
using Core.Utilities.Results;
using Core.Entities;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private ICustomerService _costumerService;

        public CustomersController(ICustomerService costumerService)
        {
            _costumerService = costumerService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pageable<Customer>), 200)]
        public IActionResult GetAll([FromQuery] int customerId, [FromQuery] int userId, [FromQuery] string companyName = "", int pageIndex = 0, int pageCount = 20)
        {
            
            var result = _costumerService.Search(companyName, customerId, userId, pageIndex, pageCount);
            
                return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Add([FromBody] CreateCustomerRequest resquest)
        {
            var result = _costumerService.Add(resquest);
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
            var result = _costumerService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateCustomerRequest request)
        {
            var result = _costumerService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}