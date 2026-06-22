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
    public record GetAllFightsQuery() : IRequest<List<FightResponseDto>>;


    public class GetAllFightsQueryHandler(IFightRepository fightRepository) : IRequestHandler<GetAllFightsQuery, List<FightResponseDto>>
    {
        public async Task<List<FightResponseDto>> Handle(GetAllFightsQuery request, CancellationToken cancellationToken)
        {
            var fights = await fightRepository.GetAllAsync();
            return fights.Select(f => new FightResponseDto
            {
                Id = f.Id,
                EventId = f.EventId,
                Status = f.Status.ToString(),
                FighterAId = f.FighterAId,
                FighterBId = f.FighterBId,
                WinnerId = f.WinnerId,
                Method = f.Method,
                Time = f.Time
            }).ToList();
        }
    }
}
