using FluentValidation;
using Webhooks.Service.Models.Parameters;

namespace Webhooks.Service.Infrastructure.Validators
{
    public class SubscriptionParametersValidator : AbstractValidator<SubscriptionParameters>
    {
        public SubscriptionParametersValidator()
        {
            RuleFor(x => x.EventId).NotNull().WithMessage(configure => $"{configure.EventId} is required.");
            RuleFor(x => x.Url).NotEmpty().NotNull().WithMessage(configure => $"{configure.Url} is required.");
        }
    }
}
