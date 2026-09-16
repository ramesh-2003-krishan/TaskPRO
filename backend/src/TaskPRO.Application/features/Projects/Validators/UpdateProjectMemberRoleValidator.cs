using FluentValidation;
using TaskPRO.Application.features.Projects.DTOs;

namespace TaskPRO.Application.features.Projects.Validators
{
    public class UpdateProjectMemberRoleValidator : AbstractValidator<UpdateProjectRoleRequest>
    {
        public UpdateProjectMemberRoleValidator()
        {
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.");
        }
    }
}