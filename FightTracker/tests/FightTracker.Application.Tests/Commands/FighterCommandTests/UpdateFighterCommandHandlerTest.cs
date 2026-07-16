using FightTracker.Application.CachingServices;
using FightTracker.Application.Events.Command;
using FightTracker.Application.Fighters.Command;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Tests.Unit.Commands.FighterCommandTests
{
    public class UpdateFighterCommandHandlerTest
    {
        private readonly IFighterRepository _fighterRepository;
        private readonly ICachingService _cachingService;
        private readonly UpdateFighterCommandHandler _sut;
        public UpdateFighterCommandHandlerTest()
        {
            _fighterRepository = Substitute.For<IFighterRepository>();
            _cachingService = Substitute.For<ICachingService>();
            _sut = new UpdateFighterCommandHandler(_fighterRepository, _cachingService);
        }

        [Fact]
        public async Task Update_ShouldUpdateFighter_WhenFighterExist()
        {
            var command = new UpdateFighterCommand(
                Id: 2,
                FullName: "Jon Jones",
                NickName: "Bones",
                WeightClass: "HeavyWeight",
                Country: "USA",
                Wins: 28,
                Losses: 1,
                Draws: 0
             );

            var updatedFighter = new Fighter
            {
                Id = 2,
                FullName = "Jon Jones",
                NickName = "Bones",
                WeightClass = "HeavyWeight",
                Country = "USA",
                Wins = 28,
                Losses = 1,
                Draws = 0
            };

            _fighterRepository.UpdateAsync(command.Id, Arg.Any<Fighter>()).Returns(updatedFighter);

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(updatedFighter.Id);
            result.FullName.Should().Be(updatedFighter.FullName);
            result.Country.Should().Be(updatedFighter.Country);

            await _fighterRepository.Received(1).UpdateAsync(command.Id, Arg.Is<Fighter>(x =>
            x.FullName == command.FullName &&
            x.Country == command.Country));

            await _cachingService.Received(1).DeleteAsync("fighters");
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenFighterDoesNotExist()
        {
            var command = new UpdateFighterCommand(
                Id: 999,
                FullName: "Unknown",
                NickName: "null",
                WeightClass: "HeavyWeight",
                Country: "Unknown",
                Wins: 0,
                Losses: 0,
                Draws: 0
                );

            _fighterRepository.UpdateAsync(command.Id, Arg.Any<Fighter>()).Returns((Fighter?)null);

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().BeNull();

            await _cachingService.DidNotReceive().DeleteAsync(Arg.Any<string>());

        }
    }
}
