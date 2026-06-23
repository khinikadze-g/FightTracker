using FightTracker.Contracts.DTOs;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FightTracker.Application.Events.Query
{
    public record GetEventByIdQuery(int id) : IRequest<EventResponseDto?>;


    public class GetEventByIdQueryHandler(IEventRepository eventRepository) : IRequestHandler<GetEventByIdQuery, EventResponseDto?>
    {
        public async Task<EventResponseDto?> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var existingEvent = await eventRepository.GetEventByIdAsync(request.id);
            if (existingEvent == null)
            {
                return null;
            }
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
