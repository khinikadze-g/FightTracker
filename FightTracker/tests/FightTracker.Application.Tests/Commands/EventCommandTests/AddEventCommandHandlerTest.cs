using FightTracker.Application.CachingServices;
using FightTracker.Application.Events.Command;
using FightTracker.Contracts.DTOs.EventDtos;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Tests.Unit.Commands.EventCommandTests
{
    public class AddEventCommandHandlerTest
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICachingService _cachingService;
        private readonly AddEventCommandHandler _sut;
        public AddEventCommandHandlerTest()
        {
            _eventRepository = Substitute.For<IEventRepository>();
            _cachingService = Substitute.For<ICachingService>();
            _sut = new AddEventCommandHandler(_eventRepository, _cachingService);
        }

        [Fact]
        public async Task Add_SholdAddEvent_WhenCommandIsValid()
        {
            var command = new AddEventCommand(
                Name: "Noche",
                Date: new(2026, 7, 15),
                Location: "USA"
                );

            var createdEvent = new Event
            {
                Id = 33,
                Name = command.Name,
                Date = command.Date,
                Location = command.Location,
            };

            _eventRepository.AddEventAsync(Arg.Any<Event>()).Returns(createdEvent);

            var expected = new EventResponseDto
            {
                Id = createdEvent.Id,
                Name = createdEvent.Name,
                eventStatus = createdEvent.eventStatus.ToString(),
                Date = createdEvent.Date,
                Location = createdEvent.Location,
                FightIds = new()
            };

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().BeEquivalentTo(expected);

            await _eventRepository.Received(1).
                AddEventAsync(Arg.Is<Event>(x =>
                x.Name == command.Name &&
                x.Date == command.Date));
            await _cachingService.Received(1).
                DeleteAsync("events");
        }
    }
}
