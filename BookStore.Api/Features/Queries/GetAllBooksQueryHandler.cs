using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Queries
{
    public class GetAllBooksQueryHandler
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ApiResponse<List<BookResponseModel>> Handle(GetAllBooksQuery query)
        {
            var books = _context.Books
                .Where(x => x.IsActive)
                .OrderBy(x => x.Id)
                .ToList();

            var response = _mapper.Map<List<BookResponseModel>>(books);
            return new ApiResponse<List<BookResponseModel>>(response);
        }
    }
}
