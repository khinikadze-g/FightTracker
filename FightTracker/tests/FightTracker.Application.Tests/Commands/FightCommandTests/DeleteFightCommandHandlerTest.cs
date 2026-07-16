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
    public class DeleteFightCommandHandlerTest
    { 
        private readonly IFightRepository _fightRepository;
        private readonly DeleteFightByIdHandler _sut;
        public DeleteFightCommandHandlerTest()
        {
            _fightRepository = Substitute.For<IFightRepository>();
            _sut = new DeleteFightByIdHandler(_fightRepository);
        }

        [Fact]
        public async Task Delete_ShouldRemoveFight_WhenFightExist()
        {
            var command = new DeleteFightByIdCommand(2);

            var existingFight = new Fight
            {
                Id = 2,
                FighterAId = 4,
                FighterBId = 12
            };

            _fightRepository.DeleteByIdAsync(command.id).Returns(existingFight);

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(existingFight.Id);

            await _fightRepository.Received(1).DeleteByIdAsync(command.id);
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenFightDoesNotExist()
        {
            var command = new DeleteFightByIdCommand(3);

            _fightRepository.DeleteByIdAsync(command.id).Returns((Fight?) null);

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().BeNull();

            await _fightRepository.Received(1).DeleteByIdAsync(command.id);
        }
    }
}
