using FightTracker.Contracts.DTOs;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FightTracker.Application.Fights.Command
{
    public record UpdateFightResultCommand(int Id, int? WinnerId, string? Method, int? Round, string? Time) : IRequest<FightResponseDto?>;


    public class UpdateFightResultCommandHandler(IFightRepository fightRepository) : IRequestHandler<UpdateFightResultCommand, FightResponseDto?>
    {
        public async Task<FightResponseDto?> Handle(UpdateFightResultCommand request, CancellationToken cancellationToken)
        {
            var existingFight = await fightRepository.GetByIdAsync(request.Id);
            if (existingFight == null)
            {
                return null;
            }
            if(existingFight.Status != FightStatus.Completed)
            {
                return null;
            }
            existingFight.WinnerId = request.WinnerId;
            existingFight.Method = request.Method;
            existingFight.Round = request.Round;
            existingFight.Time = request.Time;

            var updatedFight = await fightRepository.UpdateFightAsync(request.Id, existingFight);
            if (updatedFight == null)
            {
                return null;
            }
            return new FightResponseDto
            {
                Id = updatedFight.Id,
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
