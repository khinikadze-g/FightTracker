using FightTracker.Contracts.DTOs;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Events.Query
{
    public class GetAllEventsQuery() : IRequest<List<EventResponseDto>>;



    public class GetAllEventsQueryHandler(IEventRepository eventRepository) : IRequestHandler<GetAllEventsQuery, List<EventResponseDto>>
    {
        public async Task<List<EventResponseDto>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await eventRepository.GetEventsAsync();
            return events.Select(e => new EventResponseDto
            {
                Id = e.Id,
                Name = e.Name,
                eventStatus = e.eventStatus.ToString(),
                Date = e.Date,
                Location = e.Location,
                FightIds = new List<int>()
            }).ToList();
  
        }
    }
}
