using FightTracker.Contracts.DTOs;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Events.Command
{
    public record UpdateEventCommand(int Id, UpdateEventDto UpdateEventDto) : IRequest<EventResponseDto?>;

    public class UpdateEventCommandHandler(IEventRepository eventRepository) : IRequestHandler<UpdateEventCommand, EventResponseDto?>
    {
        public async Task<EventResponseDto?> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await eventRepository.GetEventByIdAsync(request.Id);
            if (existingEvent == null)
            {
                return null;
            }
            if (existingEvent.eventStatus != Core.Entities.EventStatus.Scheduled)
            {
                return null;
            }
            existingEvent.Name = request.UpdateEventDto.Name;
            existingEvent.Date = request.UpdateEventDto.Date;
            existingEvent.Location = request.UpdateEventDto.Location;

            var updatedEvent = await eventRepository.UpdateEventAsync(request.Id, existingEvent);

            if (updatedEvent == null)
            {
                return null;
            }
            return new EventResponseDto
            {
                Id = updatedEvent.Id,
                Name = updatedEvent.Name,
                eventStatus = updatedEvent.eventStatus.ToString(),
                Date = updatedEvent.Date,
                Location = updatedEvent.Location
            };
        }
    }
}
