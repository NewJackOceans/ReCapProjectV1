using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using Business.Abstract;
using Entities.Requests.Rentals;
using Microsoft.AspNetCore.Authorization;
using Core.Entities;
using Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentalsController : ControllerBase
    {
        private IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pageable<Rental>), 200)]
        public IActionResult GetAll([FromQuery] int id, [FromQuery] int carId, [FromQuery] int customerId, [FromQuery] DateTime rentDate,[FromQuery] DateTime returnDate, [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            

            var result = _rentalService.Search(id, carId, customerId, rentDate, returnDate, pageIndex, pageCount);
            
                return Ok(result);
        }

        [HttpGet("details")]
        [ProducesResponseType(typeof(IDataResult<List<RentalDetailDto>>), 200)]
        public IActionResult GetRentalsDetails()
        {
            var result = _rentalService.GetRentalsDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Add([FromBody] CreateRentalRequest request)
        {
            var result = _rentalService.Add(request);
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
            var result = _rentalService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateRentalRequest request)
        {
            var result = _rentalService.Update(id, request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("iscaravaible")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult IsCarAvaible(int cardId)
        {
            var result = _rentalService.IsCarAvaible(cardId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("totalprice")]
        public IActionResult TotalPrice(object totalAmountInfo)
        {

            return Ok();

        }
    }
}