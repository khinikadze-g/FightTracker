using FightTracker.Application.Fights.Command;
using FightTracker.Application.Fights.Query;
using FightTracker.Contracts.DTOs;
using MediatR;
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
        public async Task<IActionResult> UpdateFightAsync([FromRoute]int Id, [FromBody]UpdateFightDto updateFightDto)
        {
            var result = await mediator.Send(new UpdateFightCommand(Id, updateFightDto));
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("fightresult/{Id}")]
        public async Task<IActionResult> UpdateFightResultAsync([FromRoute] int Id, [FromBody] UpdateFightResultDto updateFightResultDto)
        {
            var result = await mediator.Send(new UpdateFightResultCommand(Id, updateFightResultDto));
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
        public async Task<IActionResult> AddFightAsync([FromBody] AddFightDto addFightDto)
        {
            var result = await mediator.Send(new AddFightCommand(addFightDto));
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
        [HttpPut("{Id}/cancel")]
        public async Task<IActionResult> CancelFight([FromRoute] int Id)
        {
            var result = await mediator.Send(new CancelFightCommand(Id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
