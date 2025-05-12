using AutoMapper;
using BookStore.Api.DBOperations;
using BookStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Api.UnitTests.TestSetup;

public class CommonTestFixture
{
    public BookStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        //InMemoryDatabase
        var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
        
        Context = new BookStoreDbContext(options);
        Context.Database.EnsureCreated();

        Context.AddGenres();
        Context.AddBooks();
        Context.SaveChanges();

        //AutoMapper for when we change anything in the mapping profile, project wil not be affected
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        Mapper = config.CreateMapper();

    }
}
