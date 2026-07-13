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
    public record UpdateFightStatusCommand(int Id, string FightStatus) : IRequest<FightResponseDto>;


    public class UpdateFightStatusCommandHandler(IFightRepository fightRepository) : IRequestHandler<UpdateFightStatusCommand, FightResponseDto?>
    {
        public async Task<FightResponseDto?> Handle(UpdateFightStatusCommand request, CancellationToken cancellationToken)
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
            var statusEnum = (FightStatus)Enum.Parse(typeof(FightStatus), request.FightStatus);
            fight.Status = statusEnum;

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
