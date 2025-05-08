using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Domain.Entities;
using BookStore.Api.Features.Commands;
using BookStore.Api.Features.Queries;
using BookStore.Api.Services.Implementations;
using BookStore.Api.Services.Interfaces;
using BookStore.Base.ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, BookStoreDbContext context, IMapper mapper)
        {
            _bookService = bookService;
            _context = context;
            _mapper = mapper;
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
            var query = new GetBookByIdQuery { Id = id };
            var handler = new GetBookByIdQueryHandler(_context, _mapper);
            var result = handler.Handle(query);

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
        public IActionResult Update([FromRoute] long id, [FromBody] UpdateBookCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL and body do not match.");

            var handler = new UpdateBookCommandHandler(_context, _mapper);
            var result = handler.Handle(command);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            var command = new DeleteBookCommand { Id = id };
            var handler = new DeleteBookCommandHandler(_context);
            var result = handler.Handle(command);

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
