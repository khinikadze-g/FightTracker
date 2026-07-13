using FightTracker.Application.Fights.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Fights.FightsValidation
{
    public class AddFightCommandValidation : AbstractValidator<AddFightCommand>
    {
        public AddFightCommandValidation()
        {
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.FighterAId).NotEmpty();
            RuleFor(x => x.FighterBId).NotEmpty();


        }
    }
}
