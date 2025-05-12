using BookStore.Api.Features.Commands;
using BookStore.Api.Validators;
using FluentAssertions;

namespace BookStore.Api.UnitTests.Validators
{
    public class CreateGenreCommandValidatorTest
    {
        private readonly CreateGenreCommandValidator _validator;

        public CreateGenreCommandValidatorTest()
        {
            _validator = new CreateGenreCommandValidator();
        }

        [Theory]
        [InlineData("Fantasy")]
        [InlineData("Adventure")]
        [InlineData("Biography")]
        public void WhenValidNamesAreGiven_ShouldNotReturnErrors(string name)
        {
            // Arrange
            var command = new CreateGenreCommand { Name = name };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        public void WhenNameIsEmpty_ShouldReturnRequiredError(string name)
        {
            var command = new CreateGenreCommand { Name = name };

            var result = _validator.Validate(command);

            result.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be("Genre name is required.");
        }

        [Fact]
        public void WhenNameIsNull_ShouldReturnRequiredError()
        {
            var command = new CreateGenreCommand { Name = null };

            var result = _validator.Validate(command);

            result.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be("Genre name is required.");
        }

        [Fact]
        public void WhenNameExceeds100Characters_ShouldReturnMaxLengthError()
        {
            var command = new CreateGenreCommand { Name = new string('a', 101) };

            var result = _validator.Validate(command);

            result.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be("Genre name must be at most 100 characters.");
        }
    }
}
