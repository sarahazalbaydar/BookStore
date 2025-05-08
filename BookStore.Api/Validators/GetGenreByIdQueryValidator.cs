using BookStore.Api.Features.Queries;
using FluentValidation;

namespace BookStore.Api.Validators;

public class GetGenreByIdQueryValidator : AbstractValidator<GetGenreByIdQuery>
{
    public GetGenreByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
