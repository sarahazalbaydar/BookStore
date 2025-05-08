using BookStore.Api.Features.Commands;
using FluentValidation;

namespace BookStore.Api.Validators
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Genre name is required.")
                .MaximumLength(100).WithMessage("Genre name must be at most 100 characters.");
        }
    }
}
