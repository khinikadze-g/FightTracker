using FightTracker.Application.Events.Command;
using FightTracker.Application.Events.EventsValidation;
using FightTracker.Application.Events.Query;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using FightTracker.Contracts.DTOs.EventDtos;

namespace FightTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator mediator;

        public EventController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddEventAsync([FromBody] AddEventRequestDto addEventRequestDto)
        { 
            var result = await mediator.Send(new AddEventCommand(addEventRequestDto.Name, addEventRequestDto.Date, addEventRequestDto.Location));
            return Ok(result);
        }
        [HttpPut("events/{Id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEventAsync([FromRoute] int Id, [FromBody] UpdateEventDto updateEventDto)
        {
            var result = await mediator.Send(new UpdateEventCommand(Id, updateEventDto.Name, updateEventDto.Date, updateEventDto.Location));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetEventsAsync()
        {
            var result = await mediator.Send(new GetAllEventsQuery());
            return Ok(result);
        }
        [HttpGet("events/{Id}")]
        public async Task<IActionResult> GetEventById([FromRoute] int Id)
        {
            var result = await mediator.Send(new GetEventByIdQuery(Id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete("events/{Id}")]
        [Authorize]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int Id)
        {
            var result = await mediator.Send(new DeleteEventCommand(Id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPut("events/{Id}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateEventStatusAsync([FromRoute] int Id, [FromBody]UpdateEventStatusDto updateEventStatusDto)
        {
            var result = await mediator.Send(new UpdateEventStatusCommand(Id, updateEventStatusDto));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
