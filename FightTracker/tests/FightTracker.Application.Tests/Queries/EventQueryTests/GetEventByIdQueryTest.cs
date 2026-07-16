using FightTracker.Application.Events.Query;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Tests.Unit.Queries.EventQueryTests
{
    public class GetEventByIdQueryTest
    {
        private readonly IEventRepository _eventRepository;
        private readonly GetEventByIdQueryHandler _sut;
        public GetEventByIdQueryTest()
        {
            _eventRepository = Substitute.For<IEventRepository>();
            _sut = new GetEventByIdQueryHandler(_eventRepository);
        }

        [Fact]
        public async Task GetById_ShouldReturnEvent_WhenEventExist()
        {
            var query = new GetEventByIdQuery(1);
            var existingEvent = new Event
            {
                Id = 1,
                Name = "UFC 320",
                Date = new DateTime(2026, 10, 15),
                Location = "Las Vegas",
                eventStatus = EventStatus.Scheduled
            };
            _eventRepository.GetEventByIdAsync(query.id).Returns(existingEvent);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(existingEvent.Id);

            await _eventRepository.Received(1).GetEventByIdAsync(query.id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenEventDoesNotExist()
        {
            var query = new GetEventByIdQuery(39);

            _eventRepository.GetEventByIdAsync(query.id).Returns((Event?)null);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Should().BeNull();

            await _eventRepository.Received(1).GetEventByIdAsync(query.id);
        }
    }
}
