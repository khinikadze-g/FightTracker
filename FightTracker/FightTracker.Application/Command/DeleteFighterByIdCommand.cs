using FightTracker.Contracts.DTOs;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Command
{
    public record DeleteFighterByIdCommand(int id) : IRequest<FighterResponseDto?>;


    public class DeleteFighterByIdCommandHandler(IFighterRepository fighterRepository) : IRequestHandler<DeleteFighterByIdCommand, FighterResponseDto?>
    {
        public async Task<FighterResponseDto?> Handle(DeleteFighterByIdCommand request, CancellationToken cancellationToken)
        {
            var deletedFighter = await fighterRepository.RemoveFighterAsync(request.id);
            if (deletedFighter == null)
            {
                return null;
            }
            return new FighterResponseDto
            {
                Id = deletedFighter.Id,
                FullName = deletedFighter.FullName,
                NickName = deletedFighter.NickName,
                Country = deletedFighter.Country,
                WeightClass = deletedFighter.WeightClass,
                Wins = deletedFighter.Wins,
                Losses = deletedFighter.Losses,
                Draws = deletedFighter.Draws
            };
        }
    }
}
