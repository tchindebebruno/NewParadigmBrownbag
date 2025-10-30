using FluentValidation;
namespace SubsManager.Application.UseCases
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(x => x.FullName).MaximumLength(256);
        }
    }
}
