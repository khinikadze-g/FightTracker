using FightTracker.Application.CachingServices;
using FightTracker.Contracts.DTOs.FighterDtos;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Fighters.Command
{
    public record DeleteFighterByIdCommand(int id) : IRequest<FighterResponseDto?>;


    public class DeleteFighterByIdCommandHandler(IFighterRepository fighterRepository, ICachingService cachingService) : IRequestHandler<DeleteFighterByIdCommand, FighterResponseDto?>
    {
        public async Task<FighterResponseDto?> Handle(DeleteFighterByIdCommand request, CancellationToken cancellationToken)
        {
            var deletedFighter = await fighterRepository.RemoveFighterAsync(request.id);
            if (deletedFighter == null)
            {
                return null;
            }
            await cachingService.DeleteAsync($"fighters:{request.id}");
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
