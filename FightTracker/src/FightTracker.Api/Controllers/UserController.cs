using FightTracker.Application.Users;
using FightTracker.Contracts.DTOs.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(RegisterUserDto dto)
        {
            await mediator.Send(new RegisterUserCommand(dto.Firstname, dto.LastName, dto.Email, dto.Password));
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync(LoginUserDto dto)
        {
            var result = await mediator.Send(new LoginUserCommand(dto.Email, dto.Password));
            if(result == null)
            {
                return BadRequest("Wrong username or password");
            }
            return Ok(result);
        }
    }
}
