using FightTracker.Contracts.DTOs;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Query
{
    public record GetFightByIdQuery(int Id) : IRequest<FightResponseDto?>;


    public class GetFightByIdQueryHandler(IFightRepository fightRepository) : IRequestHandler<GetFightByIdQuery, FightResponseDto?>
    {
        public async Task<FightResponseDto?> Handle(GetFightByIdQuery request, CancellationToken cancellationToken)
        {
            var fight = await fightRepository.GetByIdAsync(request.Id);
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
