using FightTracker.Application.CachingServices;
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
    public class DeleteFighterCommandHandlerTest
    {
        private readonly IFighterRepository _fighterRepository;
        private readonly ICachingService _cachingService;
        private readonly DeleteFighterByIdCommandHandler _sut;
        public DeleteFighterCommandHandlerTest()
        {
            _fighterRepository = Substitute.For<IFighterRepository>();
            _cachingService = Substitute.For<ICachingService>();
            _sut = new DeleteFighterByIdCommandHandler(_fighterRepository, _cachingService);
        }

        [Fact]
        public async Task Delete_ShouldRemoveFighter_WhenFighterExist()
        {
            var command = new DeleteFighterByIdCommand(2);

            var ExistingFighter = new Fighter
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

            _fighterRepository.RemoveFighterAsync(command.id).Returns(ExistingFighter);
            

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.FullName.Should().Be(ExistingFighter.FullName);
            result.Country.Should().Be(ExistingFighter.Country);

            await _fighterRepository.Received(1).RemoveFighterAsync(command.id);

            await _cachingService.Received(1).DeleteAsync($"fighters:{command.id}");
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenFighterDoesNotExist()
        {
            var command = new DeleteFighterByIdCommand(8888);

            _fighterRepository.RemoveFighterAsync(command.id).Returns((Fighter?)null);

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().BeNull();

            await _fighterRepository.Received(1).RemoveFighterAsync(command.id);

            await _cachingService.DidNotReceive().DeleteAsync(Arg.Any<string>());
        }
    }
}
