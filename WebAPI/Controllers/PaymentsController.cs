using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Requests.Payments;
using Microsoft.AspNetCore.Authorization;
using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Add([FromBody] CreatePaymentRequest request)
        {
            var result = _paymentService.Add(request);
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
            var result = _paymentService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdatePaymentRequest request)
        {
            var result = _paymentService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pageable<Payment>), 200)]
        public IActionResult GetAll([FromQuery] int paymentId, [FromQuery] int customerId, int pageIndex = 0, int pageCount = 20)
        {
            var result = _paymentService.Search(paymentId, customerId, pageIndex, pageCount);
            
                return Ok(result);
        }

        
    }
}