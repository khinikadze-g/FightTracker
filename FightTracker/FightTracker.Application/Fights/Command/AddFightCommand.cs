using FightTracker.Contracts.DTOs;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Fights.Command
{
    public record AddFightCommand(AddFightDto AddFightDto) : IRequest<FightResponseDto>;


    public class AddFightCommandHandler(IFightRepository fightRepository) : IRequestHandler<AddFightCommand, FightResponseDto>
    {
        public async Task<FightResponseDto> Handle(AddFightCommand request, CancellationToken cancellationToken)
        {
            var fight = new Fight
            {
                EventId = request.AddFightDto.EventId,
                FighterAId = request.AddFightDto.FighterAId,
                FighterBId = request.AddFightDto.FighterBId
            };
            await fightRepository.AddFightAsync(fight);
            return new FightResponseDto
            {
                Id = fight.Id,
                EventId = fight.EventId,
                Status = fight.Status.ToString(),
                FighterAId = fight.FighterAId,
                FighterBId = fight.FighterBId,
                WinnerId = fight.WinnerId,
                Method = fight.Method,
                Time = fight.Time
            };
        }
    }
}
