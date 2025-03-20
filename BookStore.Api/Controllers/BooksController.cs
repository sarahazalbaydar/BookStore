using BookStore.Api.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        Book[] books = new Book[]
        {
            new Book
            {
                Title = "1984",
                Author = "George Orwell",
                ISBN = "9780451524935",
                Price = 62,
                Stock = 10,
                PageCount = 328,
                PublishedDate = new DateTime(1949, 6, 8),
                Publisher = "Secker & Warburg",
                Description = "Dystopian novel about a totalitarian regime.",
            },
            new Book
            {
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                ISBN = "9780061120084",
                Price = 42,
                Stock = 5,
                PageCount = 281,
                PublishedDate = new DateTime(1960, 7, 11),
                Publisher = "J.B. Lippincott & Co.",
                Description = "A novel about racial injustice in the American South.",
            },
            new Book
            {
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                ISBN = "9780743273565",
                Price = 48,
                Stock = 3,
                PageCount = 180,
                PublishedDate = new DateTime(1925, 4, 10),
                Publisher = "Charles Scribner's Sons",
                Description = "A story of wealth, love, and the American dream.",
            }
        };

        [HttpGet("GetAll")]
        public IEnumerable<Book> GetBooks()
        {
            return books;
        }

        //[HttpGet("GetById/{id}")]
        //public Book GetBookById([FromRoute] long id)
        //{
        //    return books[Convert.ToInt32(id) % 3]; // Just to simulate different books
        //}

        [HttpGet]
        public Book GetBookById([FromQuery] long id)
        {
            return books[Convert.ToInt32(id) % 3]; // Just to simulate different books
        }

        [HttpPost]
        public Book Post([FromBody] Book book)
        {
            return book;
        }

        [HttpPut("{id}")]
        public Book Put([FromRoute]long id, [FromBody] Book book)
        {
            return book;
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] long id)
        {
        }

        //example of a PATCH request(for partial update)
        [HttpPatch("PatchByPriceOrStock/{id}")]
        public IActionResult PatchBookByPriceOrStock(long id, [FromBody] Dictionary<string, object> updates)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
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
                                return BadRequest(new { message = "Price must be greater than zero." });
                            }
                            book.Price = price;
                        }
                        else
                        {
                            return BadRequest(new { message = "Invalid price value." });
                        }
                        break;

                    case "stock":
                        if (int.TryParse(updates[key]?.ToString(), out int stock) && stock >= 0)
                            book.Stock = stock;
                        else
                        {
                            return BadRequest(new { message = "Stock must be a non-negative integer." });
                        }
                            break;
                    default:
                        return BadRequest(new { message = $"Field '{key}' cannot be updated or does not exist." });
                }
            }

            return Ok(book);
        }

        //example of sorting books by price
        [HttpGet("SortByPrice/{isAscending}")]
        public IActionResult SortBooksByPrice([FromRoute] bool isAscending = true)
        {
            var sortedBooks = isAscending
                ? books.OrderBy(b => b.Price).ToList()
                : books.OrderByDescending(b => b.Price).ToList();

            return Ok(sortedBooks);
        }

    }
}
