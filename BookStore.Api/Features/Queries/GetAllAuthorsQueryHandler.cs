using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Queries;

public class GetAllAuthorsQueryHandler
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAllAuthorsQueryHandler(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<List<AuthorResponseModel>> Handle(GetAllAuthorsQuery query)
    {
        var authors = _context.Authors
            .Where(a => a.IsActive)
            .OrderBy(a => a.Id)
            .ToList();

        var response = _mapper.Map<List<AuthorResponseModel>>(authors);
        return new ApiResponse<List<AuthorResponseModel>>(response);
    }
}
