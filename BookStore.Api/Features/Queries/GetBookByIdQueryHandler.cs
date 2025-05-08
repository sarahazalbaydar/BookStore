using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Queries;

public class GetBookByIdQueryHandler
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHandler(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<BookResponseModel> Handle(GetBookByIdQuery query)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == query.Id);
        if (book == null)
            return new ApiResponse<BookResponseModel>("Book not found");

        var response = _mapper.Map<BookResponseModel>(book);
        return new ApiResponse<BookResponseModel>(response);
    }
}
