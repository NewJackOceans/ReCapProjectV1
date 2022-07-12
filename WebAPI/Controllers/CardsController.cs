using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Requests.Cards;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [HttpPost]
        public IActionResult Add([FromBody] CreateCardRequest request)
        {
            var result = _cardService.Add(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _cardService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody] UpdateCardRequest request)
        {
            var result = _cardService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int cardId, [FromQuery] int customerId, [FromQuery] string ownerName="", [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 20)
        {
            var result = _cardService.Search(ownerName, cardId, customerId, pageIndex, pageSize);
            
                return Ok(result);
        }

        
    }
}