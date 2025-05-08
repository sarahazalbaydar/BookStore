using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Queries;

public class GetGenreByIdQueryHandler
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenreByIdQueryHandler(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<GenreResponseModel> Handle(GetGenreByIdQuery query)
    {
        var genre = _context.Genres.FirstOrDefault(x => x.Id == query.Id && x.IsActive);
        if (genre == null)
            return new ApiResponse<GenreResponseModel>("Genre not found");

        var response = _mapper.Map<GenreResponseModel>(genre);
        return new ApiResponse<GenreResponseModel>(response);
    }
}
