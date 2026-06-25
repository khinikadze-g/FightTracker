using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FightTracker.Application.Events.EventsValidation
{
    public class EventValidationBehaviour<TRequest, Tresponse> : IPipelineBehavior<TRequest, Tresponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public EventValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }
        public async Task<Tresponse> Handle(TRequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failtures = validationResult.Where(v => !v.IsValid).SelectMany(x => x.Errors).ToList();
            if (failtures.Any())
            {
                throw new ValidationException(failtures);
            }
            return await next();
        }
    }
}
