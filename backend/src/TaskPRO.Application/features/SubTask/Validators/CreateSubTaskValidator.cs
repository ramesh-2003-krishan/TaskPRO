using FluentValidation;
using TaskPRO.Application.features.SubTask.DTOs;

namespace TaskPRO.Application.features.SubTask.Validators
{
    public class CreateSubTaskValidator : AbstractValidator<CreateSubTaskRequest>
    {
        public CreateSubTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(x => x.TaskItemId)
                .NotEmpty().WithMessage("TaskItemId is required.");
        }
    }
}