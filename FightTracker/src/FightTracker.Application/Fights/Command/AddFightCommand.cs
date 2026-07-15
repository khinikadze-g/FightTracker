using FightTracker.Contracts.DTOs.FightDtos;
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
    public record AddFightCommand(int EventId, int FighterAId, int FighterBId) : IRequest<FightResponseDto>;


    public class AddFightCommandHandler(IFightRepository fightRepository) : IRequestHandler<AddFightCommand, FightResponseDto>
    {
        public async Task<FightResponseDto> Handle(AddFightCommand request, CancellationToken cancellationToken)
        {
            var fight = new Fight
            {
                EventId = request.EventId,
                FighterAId = request.FighterAId,
                FighterBId = request.FighterBId
            };
            var createdFight = await fightRepository.AddFightAsync(fight);
            return new FightResponseDto
            {
                Id = createdFight.Id,
                EventId = createdFight.EventId,
                Status = createdFight.Status.ToString(),
                FighterAId = createdFight.FighterAId,
                FighterBId = createdFight.FighterBId,
                WinnerId = createdFight.WinnerId,
                Method = createdFight.Method,
                Time = createdFight.Time
            };
        }
    }
}
