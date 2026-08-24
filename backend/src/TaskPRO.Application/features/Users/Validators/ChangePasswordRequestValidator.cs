using FluentValidation;
using TaskPRO.Application.features.Users.DTOs;

namespace TaskPRO.Application.features.Users.Validators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("New password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("New password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("New password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("New password must contain at least one special character.");
        }
    }
}