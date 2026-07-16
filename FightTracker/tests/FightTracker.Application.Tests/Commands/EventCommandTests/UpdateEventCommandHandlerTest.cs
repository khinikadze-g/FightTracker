using FightTracker.Application.CachingServices;
using FightTracker.Application.Events.Command;
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
    public class UpdateEventCommandHandlerTest
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICachingService _cachingService;
        private readonly UpdateEventCommandHandler _sut;
        public UpdateEventCommandHandlerTest()
        {
            _eventRepository = Substitute.For<IEventRepository>();
            _cachingService = Substitute.For<ICachingService>();
            _sut = new UpdateEventCommandHandler(_eventRepository, _cachingService);
        }

        [Fact]
        public async Task Update_ShouldUpdateEvent_WhenEventExist()
        {
            var command = new UpdateEventCommand(
                Id: 5,
                Name: "UFC 329",
                Date: new DateTime(2026, 07, 11),
                Location: "USA"
                );

            var existingEvent = new Event
            {
                Id = 5,
                Name = "Old name",
                Date = new DateTime(2026, 07, 10),
                Location = "USA",
                eventStatus = EventStatus.Scheduled
            };

            _eventRepository.GetEventByIdAsync(command.Id).Returns(existingEvent);

            _eventRepository.UpdateEventAsync(command.Id, Arg.Any<Event>()).Returns(existingEvent);

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);
            result.Name.Should().Be(command.Name);
            result.Date.Should().Be(command.Date);
            result.Location.Should().Be(command.Location);

            await _eventRepository.Received(1).UpdateEventAsync(command.Id, Arg.Is<Event>(x =>
            x.Id == command.Id &&
            x.Name == command.Name &&
            x.Date == command.Date &&
            x.Location == command.Location));

        }


        [Fact]
        public async Task Update_ShouldReturnNull_WhenEventDoesNotExist()
        {
            var command = new UpdateEventCommand(
                Id: 5,
                Name: "UFC 329",
                Date: new DateTime(2026, 07, 11),
                Location: "USA"
                );

            _eventRepository.GetEventByIdAsync(command.Id).Returns((Event?)null);

            var result = await _sut.Handle(command, CancellationToken.None);
            
            result.Should().BeNull();

            await _eventRepository.DidNotReceive().UpdateEventAsync(Arg.Any<int>(), Arg.Any<Event>());

            await _cachingService.DidNotReceive().DeleteAsync(Arg.Any<string>());
        }
    }
}
