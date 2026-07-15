using FightTracker.Application.CachingServices;
using FightTracker.Contracts.DTOs.EventDtos;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Events.Command
{
    public record AddEventCommand(
    string Name,
    DateTime Date,
    string Location) : IRequest<EventResponseDto>;


    public class AddEventCommandHandler(IEventRepository eventRepository, ICachingService cachingService) : IRequestHandler<AddEventCommand, EventResponseDto>
    {
        public async Task<EventResponseDto> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = new Event
            {
                Name = request.Name,
                Date = request.Date,
                Location = request.Location,
            };
            var addedEvent = await eventRepository.AddEventAsync(eventEntity);
            await cachingService.DeleteAsync("events");
            return new EventResponseDto
            {
                Id = addedEvent.Id,
                Name = addedEvent.Name,
                eventStatus = addedEvent.eventStatus.ToString(),
                Date = addedEvent.Date,
                Location = addedEvent.Location,
                FightIds = new List<int>()
            };
        }
    }
}
