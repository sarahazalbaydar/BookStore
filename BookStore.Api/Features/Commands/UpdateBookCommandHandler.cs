using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Commands;

public class UpdateBookCommandHandler
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<BookResponseModel> Handle(UpdateBookCommand command)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == command.Id);

        if (book == null)
            return new ApiResponse<BookResponseModel>("Book not found");

        _mapper.Map(command, book);
        book.UpdatedDate = DateTime.Now;

        _context.SaveChanges();

        var response = _mapper.Map<BookResponseModel>(book);
        return new ApiResponse<BookResponseModel>(response);
    }
}
