using FightTracker.Application.Events.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Events.EventsValidation
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(30);
            RuleFor(x => x.Date).GreaterThan(DateTime.UtcNow).WithMessage("Event date must be in the future!");
            RuleFor(x => x.Location).NotEmpty();
        }
    }
}
