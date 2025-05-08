using AutoMapper;
using BookStore.Api.Domain.Entities;
using BookStore.Api.Features.Commands;
using BookStore.Api.Features.Models;
using BookStore.Api.Features.Queries;

namespace BookStore.Api.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateBookCommand, Book>();
            CreateMap<Book, BookResponseModel>();

            CreateMap<GetBookByIdQuery, Book>();
            CreateMap<Book, BookResponseModel>();
        }

    }
}
