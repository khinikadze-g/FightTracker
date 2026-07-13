using FightTracker.Application.CachingServices;
using FightTracker.Contracts.DTOs.FighterDtos;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FightTracker.Application.Fighters.Command
{
    public record AddFighterCommand( 
    string FullName,
    string? NickName,
    string WeightClass,
    string Country,
    int Wins,
    int Losses,
    int Draws) : IRequest<FighterResponseDto>;


    public class AddFighterCommandHandler(IFighterRepository fighterRepository, ICachingService cachingService) : IRequestHandler<AddFighterCommand, FighterResponseDto>
    {
        public async Task<FighterResponseDto> Handle(AddFighterCommand request, CancellationToken cancellationToken)
        {
            var fighter = new Fighter { 
                FullName = request.FullName,
                NickName = request.NickName,
                Country = request.Country,
                WeightClass = request.WeightClass,
                Wins = request.Wins,
                Losses = request.Losses,
                Draws = request.Draws
                };
            var createdFighter = await fighterRepository.AddFighterAsync(fighter);
            await cachingService.DeleteAsync("fighters");
            return new FighterResponseDto
            {
                Id = createdFighter.Id,
                FullName = createdFighter.FullName,
                NickName = createdFighter.NickName,
                Country = createdFighter.Country,
                WeightClass = createdFighter.WeightClass,
                Wins = createdFighter.Wins,
                Losses = createdFighter.Losses,
                Draws = createdFighter.Draws
            };
        }
    }
}
