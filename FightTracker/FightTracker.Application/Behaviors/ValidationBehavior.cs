using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FightTracker.Application.Behaviors
{
    public class ValidationBehavior<TRequest, Tresponse> : IPipelineBehavior<TRequest, Tresponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }
        public async Task<Tresponse> Handle(TRequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResult.Where(v => !v.IsValid).SelectMany(x => x.Errors).ToList();
            if (failures.Any())
            {
                throw new ValidationException(failures);
                throw new ValidationException(failures);
            }
            return await next();
        }
    }
}
