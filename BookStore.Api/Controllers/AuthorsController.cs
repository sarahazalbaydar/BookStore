using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Commands;
using BookStore.Api.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public AuthorsController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var query = new GetAllAuthorsQuery();
        var handler = new GetAllAuthorsQueryHandler(_context, _mapper);
        var result = handler.Handle(query);

        if (!result.Success || result.Response == null || result.Response.Count == 0)
            return NotFound(result);

        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById([FromRoute] long id)
    {
        var query = new GetAuthorByIdQuery { Id = id };
        var handler = new GetAuthorByIdQueryHandler(_context, _mapper);
        var result = handler.Handle(query);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateAuthorCommand command)
    {
        var handler = new CreateAuthorCommandHandler(_context, _mapper);
        var result = handler.Handle(command);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] long id, [FromBody] UpdateAuthorCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id in route and body must match.");

        var handler = new UpdateAuthorCommandHandler(_context, _mapper);
        var result = handler.Handle(command);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] long id)
    {
        var command = new DeleteAuthorCommand { Id = id };
        var handler = new DeleteAuthorCommandHandler(_context);
        var result = handler.Handle(command);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}
