using BookStore.Api.DBOperations;
using BookStore.Api.Domain.Entities;
using BookStore.Api.Services.Implementations;
using BookStore.Api.Services.Interfaces;
using BookStore.Base.ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _bookService.GetAllBooks();
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById([FromRoute] long id)
        {
            var result = _bookService.GetBookById(id);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            var result = _bookService.CreateBook(newBook);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] long id, [FromBody] Book book)
        {
            var result = _bookService.UpdateBook(id, book);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            var result = _bookService.DeleteBook(id);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("PatchPriceOrStock2/{id}")]
        public IActionResult PatchPriceOrStock([FromRoute] long id, [FromBody] Dictionary<string, object> updates)
        {
            var result = _bookService.PatchBookPriceOrStock(id, updates);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("SortByPrice2")]
        public IActionResult SortByPrice([FromQuery] bool isAscending)
        {
            var result = _bookService.SortBooksByPrice(isAscending);
            return Ok(result);
        }
    }
}
