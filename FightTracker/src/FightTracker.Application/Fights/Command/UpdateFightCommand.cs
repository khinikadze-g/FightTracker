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
    public record UpdateFightCommand(int Id, int FighterAId, int FighterBId) : IRequest<FightResponseDto?>;

    public class UpdateFightCommandHandler(IFightRepository fightRepository) : IRequestHandler<UpdateFightCommand, FightResponseDto?>
    {
        public async Task<FightResponseDto?> Handle(UpdateFightCommand request, CancellationToken cancellationToken)
        {
            var existingFight = await fightRepository.GetByIdAsync(request.Id);
            if (existingFight == null)
            {
                return null;
            }
            if (existingFight.Status != FightStatus.Scheduled)
            {
                return null;
            }


            existingFight.FighterAId = request.FighterAId;
            existingFight.FighterBId = request.FighterBId;
            
            var updatedFight = await fightRepository.UpdateFightAsync(request.Id, existingFight);

            if (updatedFight == null)
            {
                return null;
            }
            return new FightResponseDto
            {   Id = updatedFight.Id,
                EventId = updatedFight.EventId,
                Status = updatedFight.Status.ToString(),
                FighterAId = updatedFight.FighterAId,
                FighterBId = updatedFight.FighterBId,
                WinnerId = updatedFight.WinnerId,
                Method = updatedFight.Method,
                Time = updatedFight.Time
            };

        }
    }
}
