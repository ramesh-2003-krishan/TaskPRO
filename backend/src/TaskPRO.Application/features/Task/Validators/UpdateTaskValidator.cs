using FluentValidation;
using TaskPRO.Application.features.Task.DTOs;

namespace TaskPRO.Application.features.Task.Validators
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");
        }
    }
}