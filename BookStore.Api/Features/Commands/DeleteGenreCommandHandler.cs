using BookStore.Api.DBOperations;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Commands;

public class DeleteGenreCommandHandler
{
    private readonly BookStoreDbContext _context;

    public DeleteGenreCommandHandler(BookStoreDbContext context)
    {
        _context = context;
    }

    public ApiResponse Handle(DeleteGenreCommand command)
    {
        var genre = _context.Genres.FirstOrDefault(x => x.Id == command.Id);
        if (genre == null)
            return new ApiResponse("Genre not found");

        if (!genre.IsActive)
            return new ApiResponse("Genre already inactive");

        genre.IsActive = false;
        genre.UpdatedDate = DateTime.Now;

        _context.SaveChanges();

        return new ApiResponse(); // Success = true
    }
}
