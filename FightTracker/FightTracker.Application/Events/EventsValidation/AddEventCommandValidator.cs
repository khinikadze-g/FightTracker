using FightTracker.Application.Events.Command;
using FightTracker.Contracts.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Events.EventsValidation
{
    public class AddEventCommandValidator : AbstractValidator<AddEventCommand>
    {
        public AddEventCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(30);
            RuleFor(x => x.Date).GreaterThan(DateTime.UtcNow).WithMessage("Event date must be in the future!");
            RuleFor(x => x.Location).NotEmpty();
        }
    }
}
