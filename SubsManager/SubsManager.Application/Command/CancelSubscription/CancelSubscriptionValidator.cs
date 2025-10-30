using FluentValidation;

namespace SubsManager.Application.UseCases
{
    public class CancelSubscriptionValidator : AbstractValidator<CancelSubscriptionCommand>
    { public CancelSubscriptionValidator() => RuleFor(x => x.SubscriptionId).NotEmpty(); }
}
