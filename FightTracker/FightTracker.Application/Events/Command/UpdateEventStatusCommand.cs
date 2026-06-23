using FightTracker.Contracts.DTOs;
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
    public record UpdateEventStatusCommand(int Id, UpdateEventStatusDto updateEventStatus) : IRequest<EventResponseDto?>;


    public class UpdateEventStatusCommandHandler(IEventRepository eventRepository) : IRequestHandler<UpdateEventStatusCommand, EventResponseDto?>
    {
        public async Task<EventResponseDto?> Handle(UpdateEventStatusCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await eventRepository.GetEventByIdAsync(request.Id);
            if (existingEvent == null)
            {
                return null;
            }
            var statusEnum = (EventStatus)Enum.Parse(typeof(EventStatus), request.updateEventStatus.EventStatus);
            
            existingEvent.eventStatus = statusEnum;
            
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
