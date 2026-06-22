using FightTracker.Contracts.DTOs;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FightTracker.Application.Command
{
    public record AddFighterCommand(Contracts.DTOs.AddFighterDto FighterDto) : IRequest<FighterResponseDto>;


    public class AddFighterCommandHandler(IFighterRepository fighterRepository) : IRequestHandler<AddFighterCommand, FighterResponseDto>
    {
        public async Task<FighterResponseDto> Handle(AddFighterCommand request, CancellationToken cancellationToken)
        {
            var fighter = new Fighter { 
                FullName = request.FighterDto.FullName,
                NickName = request.FighterDto.NickName,
                Country = request.FighterDto.Country,
                WeightClass = request.FighterDto.WeightClass,
                Wins = request.FighterDto.Wins,
                Losses = request.FighterDto.Losses,
                Draws = request.FighterDto.Draws
                };
            var createdFighter = await fighterRepository.AddFighterAsync(fighter);
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
