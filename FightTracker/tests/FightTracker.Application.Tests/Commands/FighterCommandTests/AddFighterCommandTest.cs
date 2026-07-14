using FightTracker.Application.CachingServices;
using FightTracker.Application.Fighters.Command;
using FightTracker.Contracts.DTOs.FighterDtos;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Tests.Commands.FighterCommandTests
{
    public class AddFighterCommandTest
    {
        private readonly IFighterRepository _fighterRepository;
        private readonly ICachingService _cachingService;
        private readonly AddFighterCommandHandler addFighterCommandHandler;

        public AddFighterCommandTest()
        {
            _fighterRepository = Substitute.For<IFighterRepository>();
            _cachingService = Substitute.For<ICachingService>();
            this.addFighterCommandHandler = new AddFighterCommandHandler(_fighterRepository, _cachingService);
        }

        [Fact]
        public async Task Add_ShouldAddFighter_WhenInputIsValid()
        {
            var command = new AddFighterCommand(
                FullName: "Jon Jones",
                NickName: "Bones",
                WeightClass: "Heavyweight",
                Country: "USA",
                Wins: 27,
                Losses: 1,
                Draws: 0
                );

            var createdFighter = new Fighter
            {
                Id = 1,
                FullName = command.FullName,
                NickName = command.NickName, 
                WeightClass = command.WeightClass,
                Country = command.Country,
                Wins = command.Wins,
                Losses = command.Losses,
                Draws = command.Draws
            };

            _fighterRepository.AddFighterAsync(Arg.Any<Fighter>()).Returns(createdFighter);

            var expected = new FighterResponseDto
            {
                Id = createdFighter.Id,
                FullName = createdFighter.FullName,
                NickName = createdFighter.NickName,
                WeightClass = createdFighter.WeightClass,
                Country = createdFighter.Country,
                Wins = createdFighter.Wins,
                Losses = createdFighter.Losses,
                Draws = createdFighter.Draws
            };


            var result = await addFighterCommandHandler.Handle(command, CancellationToken.None);

            result.Should().BeEquivalentTo(expected);

            await _fighterRepository.Received(1)
            .AddFighterAsync(Arg.Is<Fighter>(f =>
                f.FullName == command.FullName &&
                f.NickName == command.NickName &&
                f.WeightClass == command.WeightClass &&
                f.Country == command.Country &&
                f.Wins == command.Wins &&
                f.Losses == command.Losses &&
                f.Draws == command.Draws));

            await _cachingService.Received(1)
                .DeleteAsync("fighters");
        }
    }
}
