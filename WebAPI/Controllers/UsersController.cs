using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Core.Entities.Requests.Users;
using Core.Utilities.Results;
using Core.Entities.Concrete;
using Entities.DTOs;
using Core.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pageable<User>), 200)]
        public IActionResult GetAll([FromQuery] int id, [FromQuery] bool status, [FromQuery] string firstName = "", [FromQuery] string lastName = "", [FromQuery] string email = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageCount = 20)
        {

            var result = _userService.Search(id, status, firstName, lastName, email, pageIndex, pageCount);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IResult), 200)]
        public IActionResult Update([FromRoute] int id, [FromBody] UserForRegisterDto userForResgisterDto)
        {
            var result = _userService.Update(id, userForResgisterDto);
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
            var result = _userService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}