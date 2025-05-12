using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Commands;
using BookStore.Api.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Api.UnitTests.Features.Commands
{
    public class CreateGenreCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Handle_WhenGenreNameAlreadyExists_ShouldReturnError()
        {
            // Arrange
            var command = new CreateGenreCommand
            {
                Name = "Dystopian" // Test verisinde var
            };

            var handler = new CreateGenreCommandHandler(_context, _mapper);

            // Act
            var result = handler.Handle(command);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Genre already exists");
            result.Response.Should().BeNull();
        }

        [Fact]
        public void Handle_WhenGenreNameIsUnique_ShouldCreateGenreAndReturnSuccess()
        {
            // Arrange
            var command = new CreateGenreCommand
            {
                Name = "Philosophy"
            };

            var handler = new CreateGenreCommandHandler(_context, _mapper);

            // Act
            var result = handler.Handle(command);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Response.Should().NotBeNull();
            result.Response.Name.Should().Be("Philosophy");

            var genreInDb = _context.Genres.FirstOrDefault(g => g.Name == "Philosophy");
            genreInDb.Should().NotBeNull();
            genreInDb.IsActive.Should().BeTrue();
        }
    }
}
