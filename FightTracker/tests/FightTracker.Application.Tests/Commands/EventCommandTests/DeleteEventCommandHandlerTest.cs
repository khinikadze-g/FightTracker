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
    public class DeleteEventCommandHandlerTest
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICachingService _cachingService;
        private readonly DeleteEventCommandHandler _sut;
        public DeleteEventCommandHandlerTest()
        {
            _eventRepository = Substitute.For<IEventRepository>();
            _cachingService = Substitute.For<ICachingService>();
            _sut = new DeleteEventCommandHandler(_eventRepository, _cachingService);
        }

        [Fact]
        public async Task Delete_ShouldDeleteEvent_WhenEventExists()
        {
            var command = new DeleteEventCommand(1);
            var existingEvent = new Event
            {
                Id = 1,
                Name = "UFC 320",
                Date = new DateTime(2026, 10, 15),
                Location = "Las Vegas"
            };

            _eventRepository.GetEventByIdAsync(command.id).Returns(existingEvent);

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Name.Should().Be(existingEvent.Name);
            result.Date.Should().Be(existingEvent.Date);
            result.Location.Should().Be(existingEvent.Location);

            await _eventRepository.Received(1).DeleteEventAsync(command.id);

            await _cachingService.Received(1).DeleteAsync($"events:{command.id}");
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenEventDoesNotExists()
        {
            var command = new DeleteEventCommand(1);
            
            _eventRepository.GetEventByIdAsync(command.id).Returns((Event?)null);

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().BeNull();

            await _eventRepository.DidNotReceive().DeleteEventAsync(Arg.Any<int>());
            
            await _cachingService.DidNotReceive().DeleteAsync(Arg.Any<string>());
        }
    }
}
