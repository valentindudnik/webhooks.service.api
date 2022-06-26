using FluentValidation;
using Webhooks.Service.Models.Parameters;

namespace Webhooks.Service.Infrastructure.Validators
{
    public class WebhookParametersValidator : AbstractValidator<WebhookParameters>
    {
        public WebhookParametersValidator()
        {
            RuleFor(x => x.WebhookMethod).NotNull().WithMessage(configure => $"{configure.WebhookMethod} is required.");
            RuleFor(x => x.EventId).NotEmpty().NotNull().WithMessage(configure => $"{configure.EventId} is required.");
        }
    }
}
