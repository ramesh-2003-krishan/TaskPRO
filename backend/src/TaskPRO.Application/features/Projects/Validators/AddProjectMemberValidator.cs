using FluentValidation;
using TaskPRO.Application.features.Projects.DTOs;

namespace TaskPRO.Application.features.Projects.Validators
{
    public class AddProjectMemberValidator : AbstractValidator<AddProjectMemberRequest>
    {
        public AddProjectMemberValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.");
        }
    }
}
