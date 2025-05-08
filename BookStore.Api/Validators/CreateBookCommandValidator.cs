using BookStore.Api.Features.Commands;
using FluentValidation;

namespace BookStore.Api.Validators;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Author).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ISBN).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
        RuleFor(x => x.GenreId).GreaterThan(0);
        RuleFor(x => x.PublishedDate).NotEmpty();
        RuleFor(x => x.CoverImageUrl).NotEmpty().Must(url => url.StartsWith("http"))
            .WithMessage("CoverImageUrl must be a valid URL.");
    }
}
