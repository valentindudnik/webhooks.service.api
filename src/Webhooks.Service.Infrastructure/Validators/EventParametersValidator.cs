using FluentValidation;
using Webhooks.Service.Models.Parameters;

namespace Webhooks.Service.Infrastructure.Validators
{
    public class EventParametersValidator : AbstractValidator<EventParameters>
    {
        public EventParametersValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage(configure => $"{configure.Name} is required.");
            RuleFor(x => x.EventType).NotEmpty().NotNull().WithMessage(configure => $"{configure.EventType} is required.");
        }
    }
}
