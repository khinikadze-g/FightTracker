using FightTracker.Application.Fighters.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Fighters.FightersValidation
{
    public class UpdateFighterCommandValidator : AbstractValidator<UpdateFighterCommand>
    {
        public UpdateFighterCommandValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MinimumLength(3).MaximumLength(40);
            RuleFor(x => x.WeightClass).NotEmpty().WithMessage("WeightClass field is required");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country field is required");
            RuleFor(x => x.Wins).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Losses).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Draws).GreaterThanOrEqualTo(0);
        }
    }
}
