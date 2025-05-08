using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Features.Models;
using BookStore.Base.ApiResponse;

namespace BookStore.Api.Features.Commands;

public class UpdateAuthorCommandHandler
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateAuthorCommandHandler(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<AuthorResponseModel> Handle(UpdateAuthorCommand command)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == command.Id);
        if (author == null)
            return new ApiResponse<AuthorResponseModel>("Author not found");

        _mapper.Map(command, author);
        author.UpdatedDate = DateTime.Now;

        _context.SaveChanges();

        var response = _mapper.Map<AuthorResponseModel>(author);
        return new ApiResponse<AuthorResponseModel>(response);
    }
}
