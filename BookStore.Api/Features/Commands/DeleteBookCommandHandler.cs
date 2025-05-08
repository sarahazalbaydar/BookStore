using BookStore.Api.DBOperations;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Commands;

public class DeleteBookCommandHandler
{
    private readonly BookStoreDbContext _context;

    public DeleteBookCommandHandler(BookStoreDbContext context)
    {
        _context = context;
    }

    public ApiResponse Handle(DeleteBookCommand command)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == command.Id);
        if (book == null)
            return new ApiResponse("Book not found");

        if (!book.IsActive)
            return new ApiResponse("Book already inactive");

        book.IsActive = false;
        book.UpdatedDate = DateTime.Now;

        _context.SaveChanges();
        return new ApiResponse(); // Success = true
    }
}