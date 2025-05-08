using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Queries;

public class GetAllGenresQueryHandler
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAllGenresQueryHandler(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<List<GenreResponseModel>> Handle(GetAllGenresQuery query)
    {
        var genres = _context.Genres
            .Where(x => x.IsActive)
            .OrderBy(x => x.Id)
            .ToList();

        var response = _mapper.Map<List<GenreResponseModel>>(genres);
        return new ApiResponse<List<GenreResponseModel>>(response);
    }
}
