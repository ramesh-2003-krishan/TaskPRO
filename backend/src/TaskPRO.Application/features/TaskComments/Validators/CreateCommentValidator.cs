using TaskPRO.Application.features.TaskComments.DTOs;
using FluentValidation;

namespace TaskPRO.Application.features.TaskComments.Validators
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(2000).WithMessage("Content cannot exceed 2000 characters.");
        }
    }
}