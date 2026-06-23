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
    public record AddEventCommand(AddEventRequestDto AddEventRequestDto) : IRequest<EventResponseDto>;


    public class AddEventCommandHandler(IEventRepository eventRepository) : IRequestHandler<AddEventCommand, EventResponseDto>
    {
        public async Task<EventResponseDto> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = new Event
            {
                Name = request.AddEventRequestDto.Name,
                Date = request.AddEventRequestDto.Date,
                Location = request.AddEventRequestDto.Location,
            };
            await eventRepository.AddEventAsync(eventEntity);
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
