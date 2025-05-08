using BookStore.Api.DBOperations;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Queries;

public class GetBookByIdQueryHandler
{
    private readonly BookStoreDbContext _context;

    public GetBookByIdQueryHandler(BookStoreDbContext context)
    {
        _context = context;
    }

    public ApiResponse<BookResponseModel> Handle(GetBookByIdQuery query)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == query.Id);
        if (book == null)
            return new ApiResponse<BookResponseModel>("Book not found");

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
