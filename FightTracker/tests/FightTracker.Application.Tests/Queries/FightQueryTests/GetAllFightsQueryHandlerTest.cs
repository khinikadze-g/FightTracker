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

namespace FightTracker.Application.Tests.Unit.Queries.FightQueryTests
{
    public class GetAllFightsQueryHandlerTest
    {
        private readonly IFightRepository _fightRepository;
        private readonly GetAllFightsQueryHandler _sut;
        public GetAllFightsQueryHandlerTest()
        {
            _fightRepository = Substitute.For<IFightRepository>();
            _sut = new GetAllFightsQueryHandler(_fightRepository);
        }
        [Fact]
        public async Task GetAll_ShouldReturnFights_WhenFightsExist()
        {
            var query = new GetAllFightsQuery();

            var fights = new List<Fight>
            {
                new Fight
                {
                    Id = 1,
                    FighterAId = 4,
                    FighterBId = 5,
                },
                new Fight
                {
                    Id = 2,
                    FighterAId = 3,
                    FighterBId = 6,
                }
            };

            _fightRepository.GetAllAsync().Returns(fights);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Count.Should().Be(2);
            result[0].Id.Should().Be(1);

            await _fightRepository.Received(1).GetAllAsync();
        }


        [Fact]
        public async Task GetAll_ShouldReturnEmptyList_WhenFightsDoesNotExist()
        {
            var query = new GetAllFightsQuery();

            var fighs = new List<Fight>();

            _fightRepository.GetAllAsync().Returns(fighs);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Should().BeEmpty();

            await _fightRepository.Received(1).GetAllAsync();
        }
    }
}
