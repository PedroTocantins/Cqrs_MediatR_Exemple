using FluentValidation;

namespace CqrsMediatr.Application.Members.Commands.Validations;
public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("Please ensure you have entred the FistName.")
            .Length(3, 150).WithMessage("The FistName must have between 3 and 150 characters.");

        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("Please ensure you have entred the LastName.")
            .Length(4, 150).WithMessage("The LastName must have between 4 and 150 characters.");

        RuleFor(c => c.Gender)
            .NotEmpty()
            .MinimumLength(4)
            .WithMessage("The gender must be a valid information.");

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.IsActive).NotNull();


    }
}
