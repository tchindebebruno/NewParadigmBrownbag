using FluentValidation;

namespace SubsManager.Application.UseCases
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceCommand>
    { public CreateServiceValidator() => RuleFor(x => x.Name).NotEmpty().MaximumLength(128); }
}
