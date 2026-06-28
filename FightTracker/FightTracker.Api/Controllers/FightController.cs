using FightTracker.Application.Fights.Command;
using FightTracker.Application.Fights.Query;
using FightTracker.Contracts.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FightController : ControllerBase
    {
        private readonly IMediator mediator;

        public FightController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPut("fight/{Id}")]
        [Authorize]
        public async Task<IActionResult> UpdateFightAsync([FromRoute]int Id, [FromBody]UpdateFightDto dto)
        {
            var result = await mediator.Send(new UpdateFightCommand(Id, dto.FighterAId, dto.FighterBId));
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("fightresult/{Id}")]
        [Authorize]
        public async Task<IActionResult> UpdateFightResultAsync([FromRoute] int Id, [FromBody] UpdateFightResultDto dto)
        {
            var result = await mediator.Send(new UpdateFightResultCommand(Id, dto.WinnerId, dto.Method, dto.Round, dto.Time));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("fights")]
        public async Task<IActionResult> getAllAsync()
        {
            var result = await mediator.Send(new GetAllFightsQuery());
            return Ok(result);
        }
        [HttpGet("fights/{Id}")]
        public async Task<IActionResult> getAllAsync([FromRoute] int Id)
        {
            var result = await mediator.Send(new GetFightByIdQuery(Id));
            if (result == null)
            { 
                 return NotFound(); 
            }
            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddFightAsync([FromBody] AddFightDto dto)
        {
            var result = await mediator.Send(new AddFightCommand(dto.EventId, dto.FighterAId, dto.FighterBId));
            return Ok(result);
        }
        [HttpDelete("fights/{Id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int Id)
        {
            var result = await mediator.Send(new DeleteFightByIdCommand(Id));
            if (result == null)
            { 
                return NotFound(); 
            }
            return Ok(result);
        }
        [HttpPut("{Id}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus([FromRoute] int Id, [FromBody] UpdateFightStatusDto dto)
        {
            var result = await mediator.Send(new UpdateFightStatusCommand(Id, dto.FightStatus));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
