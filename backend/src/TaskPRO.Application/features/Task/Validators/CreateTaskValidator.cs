using FluentValidation;
using TaskPRO.Application.features.Task.DTOs;


namespace TaskPRO.Application.features.Task.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

        }
    }
}