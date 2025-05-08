using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Domain.Entities;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Commands;

public class CreateAuthorCommandHandler
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateAuthorCommandHandler(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<AuthorResponseModel> Handle(CreateAuthorCommand command)
    {
        var exists = _context.Authors.Any(a =>
            a.FirstName.ToLower() == command.FirstName.ToLower() &&
            a.LastName.ToLower() == command.LastName.ToLower());

        if (exists)
            return new ApiResponse<AuthorResponseModel>("Author already exists");

        var author = _mapper.Map<Author>(command);
        author.CreatedDate = DateTime.Now;

        _context.Authors.Add(author);
        _context.SaveChanges();

        var response = _mapper.Map<AuthorResponseModel>(author);
        return new ApiResponse<AuthorResponseModel>(response);
    }
}
