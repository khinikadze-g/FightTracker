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
    public record CancelFightCommand(int Id) : IRequest<FightResponseDto>;


    public class CancelFightCommandHandler(IFightRepository fightRepository) : IRequestHandler<CancelFightCommand, FightResponseDto?>
    {
        public async Task<FightResponseDto?> Handle(CancelFightCommand request, CancellationToken cancellationToken)
        {
            var fight = await fightRepository.GetByIdAsync(request.Id);
            if (fight == null)
            {
                return null;
            }
            if (fight.Status != FightStatus.Scheduled)
            {
                return null;
            }
            fight.Status = FightStatus.Cancelled;
            await fightRepository.UpdateFightAsync(request.Id, fight);
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
