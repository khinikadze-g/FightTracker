using FightTracker.Application.CachingServices;
using FightTracker.Application.Fighters.Query;
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
    public class GetAllFightersQueryHandlerTest
    {
        private readonly IFighterRepository _fighterRepository;
        private readonly GetAllFightersQueryHandler _sut;
        public GetAllFightersQueryHandlerTest()
        {
            _fighterRepository = Substitute.For<IFighterRepository>();
            _sut = new GetAllFightersQueryHandler(_fighterRepository);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllFighters_WhenFightersExist()
        {
            var query = new GetAllFightersQuery();

            var fighters = new List<Fighter>
            {
                new Fighter
                {
                    FullName = "Jon Jones",
                    NickName = "Bones",
                    WeightClass = "Heavyweight",
                    Country = "USA",
                    Wins = 27,
                    Losses = 1,
                    Draws = 0
                },

                new Fighter
                {
                    FullName = "Georges St-Pierre",
                    NickName = "Rush",
                    WeightClass = "Welterweight",
                    Country = "Canada",
                    Wins = 26,
                    Losses = 2,
                    Draws = 0
                }
            };

            _fighterRepository.GetAllAsync().Returns(fighters);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Count.Should().Be(2);
            result[1].Country.Should().Be(fighters[1].Country);

            await _fighterRepository.Received(1).GetAllAsync();

        }

        [Fact]
        public async Task GetAll_ShouldReturnNull_WhenFightersDoesNotExist()
        {
            var query = new GetAllFightersQuery();

            var fighters = new List<Fighter>();

            _fighterRepository.GetAllAsync().Returns(fighters);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Should().BeEmpty();

            await _fighterRepository.Received(1).GetAllAsync();
        }
    }
}
