using BookStore.Api.DBOperations;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Commands;

public class UpdateBookCommandHandler
{
    private readonly BookStoreDbContext _context;

    public UpdateBookCommandHandler(BookStoreDbContext context)
    {
        _context = context;
    }

    public ApiResponse<BookResponseModel> Handle(UpdateBookCommand command)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == command.Id);

        if (book == null)
            return new ApiResponse<BookResponseModel>("Book not found");

        book.Title = command.Title;
        book.Author = command.Author;
        book.ISBN = command.ISBN;
        book.Price = command.Price;
        book.Stock = command.Stock;
        book.GenreId = command.GenreId;
        book.PageCount = command.PageCount;
        book.PublishedDate = command.PublishedDate;
        book.Publisher = command.Publisher;
        book.Description = command.Description;
        book.CoverImageUrl = command.CoverImageUrl;
        book.IsActive = command.IsActive;
        book.UpdatedDate = DateTime.Now;

        _context.SaveChanges();

        var response = new BookResponseModel
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            Price = book.Price,
            Stock = book.Stock,
            GenreId = book.GenreId,
            PageCount = book.PageCount,
            PublishedDate = book.PublishedDate,
            Publisher = book.Publisher,
            Description = book.Description,
            CoverImageUrl = book.CoverImageUrl,
            IsActive = book.IsActive
        };

        return new ApiResponse<BookResponseModel>(response);
    }
}
