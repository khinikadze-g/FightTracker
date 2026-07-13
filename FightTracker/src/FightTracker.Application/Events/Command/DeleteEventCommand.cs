using FightTracker.Application.CachingServices;
using FightTracker.Contracts.DTOs.EventDtos;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Events.Command
{
    public record DeleteEventCommand(int id) : IRequest<EventResponseDto?>;


    public class DeleteEventCommandHandler(IEventRepository eventRepository, ICachingService cachingService) : IRequestHandler<DeleteEventCommand, EventResponseDto?>
    {
        public async Task<EventResponseDto?> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await eventRepository.GetEventByIdAsync(request.id);
            if (existingEvent == null)
            {
                return null;
            }
            await eventRepository.DeleteEventAsync(request.id);
            await cachingService.DeleteAsync($"events:{request.id}");
            return new EventResponseDto
            {
                Id = existingEvent.Id,
                Name = existingEvent.Name,
                eventStatus = existingEvent.eventStatus.ToString(),
                Date = existingEvent.Date,
                Location = existingEvent.Location,
                FightIds = new List<int>()
            };
        }
    }
}
