using FightTracker.Application.CachingServices;
using FightTracker.Contracts.DTOs.FighterDtos;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Fighters.Query
{
    public record GetFighterByIdQuery(int Id) : IRequest<FighterResponseDto?>, ICacheable
    {
        public string Key => $"fighters:{Id}";
        public TimeSpan Expiration => TimeSpan.FromSeconds(30);
    }

    public class GetFighterByIdQueryHandler(IFighterRepository fighterRepository) : IRequestHandler<GetFighterByIdQuery, FighterResponseDto?>
    {
        public async Task<FighterResponseDto?> Handle(GetFighterByIdQuery request, CancellationToken cancellationToken)
        {
            var fighter = await fighterRepository.GetByIdAsync(request.Id);
            if (fighter == null)
            {
                return null;
            }
            
            return new FighterResponseDto
            {
                Id = fighter.Id,
                FullName = fighter.FullName,
                NickName = fighter.NickName,
                Country = fighter.Country,
                WeightClass = fighter.WeightClass,
                Wins = fighter.Wins,
                Losses = fighter.Losses,
                Draws = fighter.Draws
            };
        }
    }
}
