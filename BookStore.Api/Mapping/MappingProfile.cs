using AutoMapper;
using BookStore.Api.Domain.Entities;
using BookStore.Api.Features.Commands;
using BookStore.Api.Features.Models;
using BookStore.Api.Features.Queries;

namespace BookStore.Api.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookResponseModel>();

        CreateMap<UpdateBookCommand, Book>();
        CreateMap<GetBookByIdQuery, Book>();
        CreateMap<DeleteBookCommand, Book>();
        CreateMap<CreateBookCommand, Book>();

        CreateMap<Genre, GenreResponseModel>();

        CreateMap<CreateGenreCommand, Genre>();
        CreateMap<UpdateGenreCommand, Genre>();

        CreateMap<Author, AuthorResponseModel>();

        CreateMap<CreateAuthorCommand, Author>();
        CreateMap<UpdateAuthorCommand, Author>();

    }
}
