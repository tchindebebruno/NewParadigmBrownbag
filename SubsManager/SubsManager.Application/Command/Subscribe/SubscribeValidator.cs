using FluentValidation;

namespace SubsManager.Application.UseCases
{
    public class SubscribeValidator : AbstractValidator<SubscribeCommand>
    { public SubscribeValidator() { RuleFor(x => x.UserId).NotEmpty(); RuleFor(x => x.PlanId).NotEmpty(); } }
}