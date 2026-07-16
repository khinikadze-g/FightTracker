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

namespace FightTracker.Application.Tests.Unit.Queries.FightQueryTests
{
    public class GetFightByIdQueryHandlerTest
    {
        private readonly IFightRepository _fightRepository;
        private readonly GetFightByIdQueryHandler _sut;
        public GetFightByIdQueryHandlerTest()
        {
            _fightRepository = Substitute.For<IFightRepository>();
            _sut = new GetFightByIdQueryHandler(_fightRepository);
        }

        [Fact]
        public async Task GetFightById_ShouldReturnFight_WhenFightExist()
        {
            var query = new GetFightByIdQuery(2);

            var existingFight = new Fight
            {
                Id = 2,
                FighterAId = 3,
                FighterBId = 4,
            };

            _fightRepository.GetByIdAsync(query.Id).Returns(existingFight);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(2);
            result.FighterAId.Should().Be(existingFight.FighterAId);
            result.FighterBId.Should().Be(existingFight.FighterBId);

            await _fightRepository.Received(1).GetByIdAsync(Arg.Any<int>());
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenFightDoesNotExist()
        {
            var query = new GetFightByIdQuery(5);

            _fightRepository.GetByIdAsync(query.Id).Returns((Fight?)null);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Should().BeNull();

            await _fightRepository.Received(1).GetByIdAsync(Arg.Any<int>());
        }
    }
}
