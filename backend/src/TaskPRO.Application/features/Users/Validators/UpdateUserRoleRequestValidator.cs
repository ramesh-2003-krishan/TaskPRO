using FluentValidation;
using TaskPRO.Application.features.Users.DTOs;

namespace TaskPRO.Application.features.Users.Validators
{
    public class UpdateUserRoleRequestValidator : AbstractValidator<UpdateUserRoleRequest>
    {
        public UpdateUserRoleRequestValidator()
        {
           RuleFor(x=> x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => role == "Admin" || role == "User").WithMessage("Role must be either 'Admin' or 'User'.");
        }
    }
}