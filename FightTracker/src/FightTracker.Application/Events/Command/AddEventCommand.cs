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
            await eventRepository.AddEventAsync(eventEntity);
            await cachingService.DeleteAsync("events");
            return new EventResponseDto
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                eventStatus = eventEntity.eventStatus.ToString(),
                Date = eventEntity.Date,
                Location = eventEntity.Location,
                FightIds = new List<int>()
            };
        }
    }
}
