using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Requests.Payments;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
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
        public IActionResult GetAll([FromQuery] int paymentId, [FromQuery] int customerId, int pageIndex = 0, int pageCount = 20)
        {
            var result = _paymentService.Search(paymentId, customerId, pageIndex, pageCount);
            
                return Ok(result);
        }

        
    }
}