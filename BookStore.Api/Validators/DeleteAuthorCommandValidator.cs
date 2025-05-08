using BookStore.Api.Features.Commands;
using FluentValidation;

namespace BookStore.Api.Validators;

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
