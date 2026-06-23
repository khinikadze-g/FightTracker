using FightTracker.Contracts.DTOs;
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
    public record UpdateFighterCommand(int Id, UpdateFighterDto UpdateFighterDto) : IRequest<FighterResponseDto?>;

    public class UpdateFighterCommandHandler(IFighterRepository fighterRepository) : IRequestHandler<UpdateFighterCommand, FighterResponseDto?>
    {
        public async Task<FighterResponseDto?> Handle(UpdateFighterCommand request, CancellationToken cancellationToken)
        {
            var fighter = new Fighter
            {
                FullName = request.UpdateFighterDto.FullName,
                NickName = request.UpdateFighterDto.NickName,
                Country = request.UpdateFighterDto.Country,
                WeightClass = request.UpdateFighterDto.WeightClass,
                Wins = request.UpdateFighterDto.Wins,
                Losses = request.UpdateFighterDto.Losses,
                Draws = request.UpdateFighterDto.Draws
            };

            var updatedFighter = await fighterRepository.UpdateAsync(request.Id,  fighter);
            if (updatedFighter == null)
            {
                return null;
            }

            return new FighterResponseDto
            {
                Id = updatedFighter.Id,
                FullName = updatedFighter.FullName,
                NickName = updatedFighter.NickName,
                Country = updatedFighter.Country,
                WeightClass = updatedFighter.WeightClass,
                Wins = updatedFighter.Wins,
                Losses = updatedFighter.Losses,
                Draws = updatedFighter.Draws
            };

        }
    }
}
