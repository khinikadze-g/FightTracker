using FightTracker.Application.Fights.Command;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Tests.Unit.Commands.FightCommandTests
{
    public class UpdateFightCommandHandlerTest
    {
        private readonly IFightRepository _fightRepository;
        private readonly UpdateFightCommandHandler _sut;
        public UpdateFightCommandHandlerTest()
        {
            _fightRepository = Substitute.For<IFightRepository>();
            _sut = new UpdateFightCommandHandler(_fightRepository);
        }

        [Fact]
        public async Task Update_ShouldUpdateFight_WhenFightExist()
        {
            var command = new UpdateFightCommand(
                Id: 1,
                FighterAId: 4,
                FighterBId: 11
                );
            var existingFight = new Fight
            {
                Id = 1,
                FighterAId = 4,
                FighterBId = 12
            };

            _fightRepository.GetByIdAsync(command.Id).Returns(existingFight);
            _fightRepository.UpdateFightAsync(command.Id, Arg.Any<Fight>()).Returns(existingFight);

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);
            result.FighterAId.Should().Be(command.FighterAId);
            result.FighterBId.Should().Be(command.FighterBId);

            await _fightRepository.Received(1).UpdateFightAsync(command.Id, Arg.Is<Fight>(x =>
            x.Id == command.Id &&
            x.FighterAId == command.FighterAId &&
            x.FighterBId == command.FighterBId));
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenFightDoesNotExist()
        {
            var command = new UpdateFightCommand(
                Id: 1,
                FighterAId: 4,
                FighterBId: 11
                );
            _fightRepository.GetByIdAsync(command.Id).Returns((Fight?)null);

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().BeNull();

            await _fightRepository.DidNotReceive().UpdateFightAsync(Arg.Any<int>(), Arg.Any<Fight>());
        }
    }
}
