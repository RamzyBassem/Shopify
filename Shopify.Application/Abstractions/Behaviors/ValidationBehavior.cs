using FluentValidation;
using MediatR;
using Shopify.Application.Abstractions.Messaging;
using Shopify.Application.Exceptions;
using ValidationException = Shopify.Application.Exceptions.ValidationException;

namespace Shopify.Application.Abstractions.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (!_validators.Any())
                return await next();
            var context = new ValidationContext<TRequest>(request);
            var validationErrors = _validators
                .Select(validator => validator.Validate(context))
                .Where(validationResult => validationResult.Errors.Any())
                .SelectMany(validationResult => validationResult.Errors)
                .Select(validationResult => new ValidationError(validationResult.PropertyName, validationResult.ErrorMessage))
                .ToList();

            if (validationErrors.Any())
            {
                throw new ValidationException(validationErrors);
            }
            return await next();
        }
    }
}
