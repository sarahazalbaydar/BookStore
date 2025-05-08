using BookStore.Api.DBOperations;
using BookStore.Api.Domain.Entities;
using BookStore.Api.Services.Interfaces;
using BookStore.Base.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace BookStore.Api.Services.Implementations;

public class EfBookService : IBookService
{

    private readonly BookStoreDbContext _context;

    public EfBookService(BookStoreDbContext context)
    {
        _context = context;
    }

    public ApiResponse<List<Book>> GetAllBooks()
    {
        var books = _context.Books.OrderBy(x => x.Id).ToList();
        if (books is null || books.Count == 0)
        {
            return new ApiResponse<List<Book>>("No books found");
        }
        return new ApiResponse<List<Book>>(books);
    }

    public ApiResponse<Book> GetBookById(long id)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);

        if (book == null)
        {
            return new ApiResponse<Book>("Book not found");
        }

        return new ApiResponse<Book>(book);
    }

    public ApiResponse<Book> CreateBook(Book newBook)
    {
        var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);

        if (book != null)
        {
            return new ApiResponse<Book>("Book already exists.");
        }

        newBook.CreatedDate = DateTime.Now;

        _context.Books.Add(newBook);
        _context.SaveChanges();

        return new ApiResponse<Book>(newBook);
    }
    public ApiResponse UpdateBook(long id, Book book)
    {
        var existingBook = _context.Books.SingleOrDefault(x => x.Id == id);

        if (existingBook is null)
        {
            return new ApiResponse("Book not found");
        }

        existingBook.GenreId = book.GenreId;
        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.ISBN = book.ISBN;
        existingBook.Price = book.Price;
        existingBook.Stock = book.Stock;
        existingBook.PageCount = book.PageCount;
        existingBook.PublishedDate = book.PublishedDate;
        existingBook.Publisher = book.Publisher;
        existingBook.Description = book.Description;
        existingBook.CoverImageUrl = book.CoverImageUrl;

        existingBook.UpdatedDate = DateTime.Now;
        existingBook.IsActive = book.IsActive;

        _context.SaveChanges();
        return new ApiResponse();
    }

    public ApiResponse DeleteBook(long id)
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if (book == null)
        {
            return new ApiResponse("Book not found");
        }
        if (book.IsActive == false)
        {
            return new ApiResponse("Book not active");
        }
        book.IsActive = false;
        book.UpdatedDate = DateTime.Now;

        _context.SaveChanges();
        return new ApiResponse();
    }


    public ApiResponse PatchBookPriceOrStock(long id, Dictionary<string, object> updates)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);
        if (book == null)
        {
            return new ApiResponse("Book not found");
        }

        foreach (var key in updates.Keys)
        {
            switch (key.ToLower())
            {
                case "price":
                    if (decimal.TryParse(updates[key]?.ToString(), out decimal price))
                    {
                        if (price < 0)
                        {
                            return new ApiResponse("Price must be greater than zero.");
                        }
                        book.Price = price;
                    }
                    else
                    {
                        return new ApiResponse("Invalid price value.");
                    }
                    break;

                case "stock":
                    if (int.TryParse(updates[key]?.ToString(), out int stock) && stock >= 0)
                        book.Stock = stock;
                    else
                    {
                        return new ApiResponse("Stock must be a non-negative integer.");
                    }
                    break;
                default:
                    return new ApiResponse( message : $"Field '{key}' cannot be updated or does not exist.");
            }
        }

        book.UpdatedDate = DateTime.Now;
        _context.SaveChanges();

        return new ApiResponse();
    }

    public ApiResponse<List<Book>> SortBooksByPrice( bool isAscending)
    {
        var sortedBooks = isAscending
            ? _context.Books.OrderBy(b => b.Price).ToList()
            : _context.Books.OrderByDescending(b => b.Price).ToList();

        return new ApiResponse<List<Book>>(sortedBooks);
    }


}
