using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.Requests.Rentals;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int id, [FromQuery] int carId, [FromQuery] int customerId, [FromQuery] DateTime rentDate,[FromQuery] DateTime retunrDate, [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {
            Thread.Sleep(1000);

            var result = _rentalService.Search(id, carId, customerId, rentDate, retunrDate, pageIndex, pageCount);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("details")]
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
        public IActionResult Add(Rental rental)
        {
            var result = _rentalService.Add(rental);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
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