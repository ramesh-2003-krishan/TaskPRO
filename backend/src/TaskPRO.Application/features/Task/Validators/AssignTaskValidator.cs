using FluentValidation;
using TaskPRO.Application.features.Task.DTOs;

namespace TaskPRO.Application.features.Task.Validators
{
    public class AssignTaskValidator : AbstractValidator<AssignTaskRequest>
    {
        public AssignTaskValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}

