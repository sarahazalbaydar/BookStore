using BookStore.Api.DBOperations;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Commands;

public class DeleteAuthorCommandHandler
{
    private readonly BookStoreDbContext _context;

    public DeleteAuthorCommandHandler(BookStoreDbContext context)
    {
        _context = context;
    }

    public ApiResponse Handle(DeleteAuthorCommand command)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == command.Id);
        if (author == null)
            return new ApiResponse("Author not found");

        if (!author.IsActive)
            return new ApiResponse("Author already inactive");

        author.IsActive = false;
        author.UpdatedDate = DateTime.Now;

        _context.SaveChanges();
        return new ApiResponse(); // Success = true
    }
}
