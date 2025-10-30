using FluentValidation;

namespace SubsManager.Application.UseCases
{
    public class CreatePlanValidator : AbstractValidator<CreatePlanCommand>
    {
        public CreatePlanValidator()
        {
            RuleFor(x => x.ServiceId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Currency).NotEmpty().Length(3);
            RuleFor(x => x.Period).Must(p => p == 1 || p == 12);
            RuleFor(x => x.TrialDays).GreaterThanOrEqualTo(0);
        }
    }
}
