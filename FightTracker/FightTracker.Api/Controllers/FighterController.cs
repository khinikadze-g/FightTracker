using FightTracker.Application.Command;
using FightTracker.Application.Query;
using FightTracker.Contracts.DTOs;
using MediatR;
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
        public async Task<IActionResult> AddFighterAsync(AddFighterDto addFighterDto)
        {
            var result = await mediator.Send(new AddFighterCommand(addFighterDto));
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
        public async Task<IActionResult> UpdateFighterAsync([FromRoute]int Id, [FromBody]UpdateFighterDto updateFighterDto)
        {
            var result = await mediator.Send(new UpdateFighterCommand(Id, updateFighterDto));
            if(result == null)
            {
                return NotFound();
            }    
            return Ok(result);
        }
        [HttpDelete("fighters/{Id}")]
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
