using FightTracker.Application.CachingServices;
using FightTracker.Application.Fighters.Query;
using FightTracker.Application.Fights.Query;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Tests.Unit.Queries.FighterQueryTests
{
    public class GetFighterByIdQueryHandlerTest
    {
        private readonly IFighterRepository _fighterRepository;
        private readonly GetFighterByIdQueryHandler _sut;
        
        public GetFighterByIdQueryHandlerTest()
        {
            _fighterRepository = Substitute.For<IFighterRepository>();
            _sut = new GetFighterByIdQueryHandler(_fighterRepository);
        }

        [Fact]
        public async Task GetById_ShouldReturnFighter_WhenFighterExist()
        {
            var query = new GetFighterByIdQuery(3);

            var fighter = new Fighter
            {
                Id = query.Id,
                FullName = "Jon Jones",
                NickName = "Bones",
                WeightClass = "Heavyweight",
                Country = "USA",
                Wins = 27,
                Losses = 1,
                Draws = 0
            };

            _fighterRepository.GetByIdAsync(query.Id).Returns(fighter);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.FullName.Should().Be(fighter.FullName);

            await _fighterRepository.Received(1).GetByIdAsync(query.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenFigterDoesNotExist()
        {
            var query = new GetFighterByIdQuery(99);

            _fighterRepository.GetByIdAsync(query.Id).Returns((Fighter?)null);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Should().BeNull();

            await _fighterRepository.Received(1).GetByIdAsync(query.Id);
        }
    }
}
