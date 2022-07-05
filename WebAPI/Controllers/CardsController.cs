﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
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
        public IActionResult Add(Card card)
        {
            var result = _cardService.Add(card);
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
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        
    }
}