using BookStore.Api.Domain.Entities;
using BookStore.Base.ApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Services.Interfaces;

public interface IBookService
{
    ApiResponse<List<Book>> GetAllBooks();
    ApiResponse<Book> GetBookById(long id);
    ApiResponse<Book> CreateBook(Book newBook);
    ApiResponse UpdateBook(long id, Book book);
    ApiResponse DeleteBook(long id);
    ApiResponse PatchBookPriceOrStock(long id, Dictionary<string, object> updates);
    ApiResponse<List<Book>> SortBooksByPrice(bool isAscending);
}
