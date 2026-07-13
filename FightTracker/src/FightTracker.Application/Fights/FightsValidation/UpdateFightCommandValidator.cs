using FightTracker.Application.Fights.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Fights.FightsValidation
{
    public class UpdateFightCommandValidator : AbstractValidator<UpdateFightCommand>
    {
        public UpdateFightCommandValidator()
        {
            RuleFor(x => x.FighterAId).NotEmpty();
            RuleFor(x => x.FighterBId).NotEmpty();

        }
    }
}
