using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

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

        [HttpGet("getall")]
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

        [HttpPost("add")]
        public IActionResult Add(Customer customer)
        {
            var result = _costumerService.Add(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Customer customer)
        {
            var result = _costumerService.Delete(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(Customer customer)
        {
            var result = _costumerService.Update(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}