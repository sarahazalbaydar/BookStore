using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Commands;

public class UpdateGenreCommandHandler
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateGenreCommandHandler(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<GenreResponseModel> Handle(UpdateGenreCommand command)
    {
        var genre = _context.Genres.FirstOrDefault(x => x.Id == command.Id);
        if (genre == null)
            return new ApiResponse<GenreResponseModel>("Genre not found");

        _mapper.Map(command, genre);
        genre.UpdatedDate = DateTime.Now;

        _context.SaveChanges();

        var response = _mapper.Map<GenreResponseModel>(genre);
        return new ApiResponse<GenreResponseModel>(response);
    }
}