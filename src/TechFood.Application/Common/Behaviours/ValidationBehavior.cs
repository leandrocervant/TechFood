using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechFood.Application.Common.Resources;

namespace TechFood.Application.Common.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest
        : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var validationContext = new ValidationContext<TRequest>(request);

            var failures = _validators
               .Select(validator => validator.Validate(request))
               .SelectMany(result => result.Errors)
               .Where(failure => failure != null);

            if (failures.Any())
            {
                throw new ValidationException(Exceptions.ValidationException_Message, failures);
            }

            return next();
        }
    }
}
