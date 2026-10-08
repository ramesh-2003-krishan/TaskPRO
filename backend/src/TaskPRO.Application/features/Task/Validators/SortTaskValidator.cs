using FluentValidation;
using TaskPRO.Application.features.Task.DTOs;

namespace TaskPRO.Application.features.Task.Validators
{
    public class SortTaskValidator : AbstractValidator<SortTaskRequest>
    {
        public SortTaskValidator()
        {
            RuleFor(x => x.SortBy)
                .NotEmpty().WithMessage("SortBy is required.")
                .Must(sortBy => sortBy == "Title" || sortBy == "DueDate" || sortBy == "Priority" || sortBy == "Status")
                .WithMessage("SortBy must be one of the following: Title, DueDate, Priority, Status.");

            RuleFor(x => x.SortOrder)
                .NotNull().WithMessage("SortOrder is required.");
        }
    }
}