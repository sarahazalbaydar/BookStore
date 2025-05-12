using BookStore.Api.DBOperations;
using BookStore.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Api.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                new Genre { Name = "Dystopian", CreatedDate = DateTime.Now, IsActive = true },
                new Genre { Name = "Classic Literature", CreatedDate = DateTime.Now, IsActive = true },
                new Genre { Name = "Historical Fiction", CreatedDate = DateTime.Now, IsActive = true },
                new Genre { Name = "Science Fiction", CreatedDate = DateTime.Now, IsActive = true },
                new Genre { Name = "Mystery", CreatedDate = DateTime.Now, IsActive = true }
            );
        }
    }
}
