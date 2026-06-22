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
    public record GetAllFightersQuery() : IRequest<List<FighterResponseDto>>;

    public class GetAllFightersQueryHandler(IFighterRepository fighterRepository) : IRequestHandler<GetAllFightersQuery, List<FighterResponseDto>>
    {
        public async Task<List<FighterResponseDto>> Handle(GetAllFightersQuery request, CancellationToken cancellationToken)
        {
            var fighters = await fighterRepository.GetAllAsync();
            return fighters.Select(f => new FighterResponseDto
            {
                Id = f.Id,
                FullName = f.FullName,
                NickName = f.NickName,
                Country = f.Country,
                WeightClass = f.WeightClass,
                Wins = f.Wins,
                Losses = f.Losses,
                Draws = f.Draws
            }).ToList();
        }
    }
}
