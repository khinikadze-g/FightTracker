using FightTracker.Contracts.DTOs.FightDtos;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Fights.Command
{
    public record DeleteFightByIdCommand(int  id) : IRequest<FightResponseDto?>;


    public class DeleteFightByIdHandler(IFightRepository fightRepository) : IRequestHandler<DeleteFightByIdCommand, FightResponseDto?>
    {
        public async Task<FightResponseDto?> Handle(DeleteFightByIdCommand request, CancellationToken cancellationToken)
        {
            var fight = await fightRepository.DeleteByIdAsync(request.id);
            if (fight == null)
            {
                return null;
            }
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
