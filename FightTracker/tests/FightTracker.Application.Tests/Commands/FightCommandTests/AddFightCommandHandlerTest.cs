using FightTracker.Application.CachingServices;
using FightTracker.Application.Fights.Command;
using FightTracker.Contracts.DTOs.FightDtos;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Tests.Unit.Commands.FightCommandTests
{
    public class AddFightCommandHandlerTest
    {
        private readonly IFightRepository _fightRepository;
        private readonly AddFightCommandHandler _sut;
        public AddFightCommandHandlerTest()
        {
            _fightRepository = Substitute.For<IFightRepository>();
            _sut = new AddFightCommandHandler(_fightRepository);
        }

        [Fact]
        public async Task Add_ShouldAddFight_WhenCommandIsValid()
        {
            var command = new AddFightCommand(
                EventId: 1,
                FighterAId: 3,
                FighterBId: 5
                );

            var createdFight = new Fight
            {
                Id = 9,
                EventId = command.EventId,
                FighterAId = command.FighterAId,
                FighterBId = command.FighterBId
            };

            _fightRepository.AddFightAsync(Arg.Any<Fight>()).Returns(createdFight);

            var expected = new FightResponseDto
            {
                Id = createdFight.Id,
                EventId = createdFight.EventId,
                FighterAId = createdFight.FighterAId,
                FighterBId = createdFight.FighterBId,
                Status = createdFight.Status.ToString(),
                WinnerId = createdFight.WinnerId,
                Method = createdFight.Method,
                Time = createdFight.Time
            };

            var result = await _sut.Handle(command, CancellationToken.None);

            result.Should().BeEquivalentTo(expected);

            await _fightRepository.Received(1).AddFightAsync(Arg.Is<Fight>(f =>
            f.EventId == command.EventId &&
            f.FighterAId == command.FighterAId &&
            f.FighterBId == command.FighterBId));
        }
    }
}
