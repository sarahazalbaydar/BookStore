using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Domain.Entities;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Commands;

public class CreateGenreCommandHandler
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateGenreCommandHandler(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<GenreResponseModel> Handle(CreateGenreCommand command)
    {
        var exists = _context.Genres.Any(x => x.Name.ToLower() == command.Name.ToLower());
        if (exists)
            return new ApiResponse<GenreResponseModel>("Genre already exists");

        var genre = _mapper.Map<Genre>(command);
        genre.IsActive = true;
        genre.CreatedDate = DateTime.Now;

        _context.Genres.Add(genre);
        _context.SaveChanges();

        var response = _mapper.Map<GenreResponseModel>(genre);
        return new ApiResponse<GenreResponseModel>(response);
    }
}
