using FluentValidation;
using MediatR;


namespace Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _valitadors;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _valitadors = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_valitadors.Any())
            {
                var context = new FluentValidation.ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_valitadors.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failure = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if( failure.Count() != 0)
                {
                    throw new Exceptions.ValidationException(failure);
                }
            }
            return await next();
        }
    }
}
