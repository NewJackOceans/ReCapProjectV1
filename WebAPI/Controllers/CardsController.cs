using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Requests.Cards;
using Microsoft.AspNetCore.Authorization;
using Core.Utilities.Results;
using Core.Entities;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(IResult), 200)]
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
        [ProducesResponseType(typeof(IResult), 200)]
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
        [ProducesResponseType(typeof(IResult), 200)]
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
        [ProducesResponseType(typeof(Pageable<Card>), 200)]
        public IActionResult GetAll([FromQuery] int cardId, [FromQuery] int customerId, [FromQuery] string ownerName="", [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 20)
        {
            var result = _cardService.Search(ownerName, cardId, customerId, pageIndex, pageSize);
            
                return Ok(result);
        }

        
    }
}