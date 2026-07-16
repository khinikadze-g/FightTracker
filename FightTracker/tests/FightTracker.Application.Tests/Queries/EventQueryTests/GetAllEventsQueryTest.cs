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
    public class GetAllEventsQueryTest
    {
        private readonly IEventRepository _eventRepository;
        private readonly GetAllEventsQueryHandler _sut;
        public GetAllEventsQueryTest()
        {
            _eventRepository = Substitute.For<IEventRepository>();
            _sut = new GetAllEventsQueryHandler(_eventRepository);
        }

        [Fact]
        public async Task GetAll_ShoultReturnEvents_WhenEventsExists()
        {
            var query = new GetAllEventsQuery();

            var events = new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Name = "UFC 320",
                    Date = new DateTime(2026,10,15),
                    Location = "Las Vegas",
                    eventStatus = EventStatus.Scheduled
                },

                new Event
                {
                    Id = 2,
                    Name = "UFC 321",
                    Date = new DateTime(2026,11,20),
                    Location = "London",
                    eventStatus = EventStatus.Completed
                }
            };

            _eventRepository.GetEventsAsync().Returns(events);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Count.Should().Be(2);
            result[0].Id.Should().Be(1);
            result[1].Location.Should().Be("London");

            await _eventRepository.Received(1).GetEventsAsync();
        }
    }
}
