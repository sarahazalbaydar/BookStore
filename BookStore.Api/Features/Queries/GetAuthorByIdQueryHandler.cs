using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Queries;

public class GetAuthorByIdQueryHandler
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorByIdQueryHandler(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<AuthorResponseModel> Handle(GetAuthorByIdQuery query)
    {
        var author = _context.Authors
            .FirstOrDefault(a => a.Id == query.Id && a.IsActive);

        if (author == null)
            return new ApiResponse<AuthorResponseModel>("Author not found");

        var response = _mapper.Map<AuthorResponseModel>(author);
        return new ApiResponse<AuthorResponseModel>(response);
    }
}
