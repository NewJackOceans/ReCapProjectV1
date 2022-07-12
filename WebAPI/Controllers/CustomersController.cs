
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Business.Abstract;
using Entities.Requests.Customers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerService _costumerService;

        public CustomersController(ICustomerService costumerService)
        {
            _costumerService = costumerService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int customerId, [FromQuery] int userId, [FromQuery] string companyName = "", int pageIndex = 0, int pageCount = 20)
        {
            Thread.Sleep(1000);

            var result = _costumerService.Search(companyName, customerId, userId, pageIndex, pageCount);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
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