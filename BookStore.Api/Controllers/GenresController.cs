using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Commands;
using BookStore.Api.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GenresController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var query = new GetAllGenresQuery();
        var handler = new GetAllGenresQueryHandler(_context, _mapper);
        var result = handler.Handle(query);

        if (!result.Success || result.Response == null || result.Response.Count == 0)
            return NotFound(result);

        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById([FromRoute] long id)
    {
        var query = new GetGenreByIdQuery { Id = id };
        var handler = new GetGenreByIdQueryHandler(_context, _mapper);
        var result = handler.Handle(query);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateGenreCommand command)
    {
        var handler = new CreateGenreCommandHandler(_context, _mapper);
        var result = handler.Handle(command);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] long id, [FromBody] UpdateGenreCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id in route and body do not match.");

        var handler = new UpdateGenreCommandHandler(_context, _mapper);
        var result = handler.Handle(command);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] long id)
    {
        var command = new DeleteGenreCommand { Id = id };
        var handler = new DeleteGenreCommandHandler(_context);
        var result = handler.Handle(command);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

}
