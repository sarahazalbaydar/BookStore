using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Domain.Entities;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Commands
{
    public class CreateBookCommandHandler
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ApiResponse<BookResponseModel> Handle(CreateBookCommand command)
        {
            var exists = _context.Books.Any(x => x.Title == command.Title && x.Author == command.Author);
            if (exists)
                return new ApiResponse<BookResponseModel>("Book already exists");

            var book = _mapper.Map<Book>(command);
            book.CreatedDate = DateTime.Now;
            book.IsActive = true;

            _context.Books.Add(book);
            _context.SaveChanges();

            var response = _mapper.Map<BookResponseModel>(book);
            return new ApiResponse<BookResponseModel>(response);
        }
    }
}
