using FightTracker.Application.Fighters.Command;
using FightTracker.Application.Fighters.Query;
using FightTracker.Contracts.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FighterController : ControllerBase
    {
        private readonly IMediator mediator;

        public FighterController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddFighterAsync(AddFighterDto dto)
        {
            var result = await mediator.Send(new AddFighterCommand(dto.FullName, dto.NickName, dto.WeightClass, dto.Country,
                dto.Wins, dto.Losses, dto.Draws));
            return Ok(result);
        }
        [HttpGet("fighters")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await mediator.Send(new GetAllFightersQuery());
            return Ok(result);
        }
        [HttpGet("fighters/{Id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int Id)
        {
            var result = await mediator.Send(new GetFighterByIdQuery(Id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPut("fighters/{Id}")]
        [Authorize]
        public async Task<IActionResult> UpdateFighterAsync([FromRoute]int Id, [FromBody]UpdateFighterDto dto)
        {
            var result = await mediator.Send(new UpdateFighterCommand(Id, dto.FullName, dto.NickName, dto.WeightClass, dto.Country,
                dto.Wins, dto.Losses, dto.Draws));
            if(result == null)
            {
                return NotFound();
            }    
            return Ok(result);
        }
        [HttpDelete("fighters/{Id}")]
        [Authorize]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int Id)
        {
            var result = await mediator.Send(new DeleteFighterByIdCommand(Id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
