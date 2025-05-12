using BookStore.Api.DBOperations;
using BookStore.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Api.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    GenreId = 1,
                    Title = "1984",
                    Author = "George Orwell",
                    ISBN = "9780451524935",
                    Price = 62,
                    Stock = 10,
                    PageCount = 328,
                    PublishedDate = new DateTime(1949, 6, 8),
                    Publisher = "Secker & Warburg",
                    Description = "Dystopian novel about a totalitarian regime.",
                    CoverImageUrl = "https://example.com/1984.jpg",
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                },
                new Book
                {
                    GenreId = 2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    ISBN = "9780061120084",
                    Price = 42,
                    Stock = 5,
                    PageCount = 281,
                    PublishedDate = new DateTime(1960, 7, 11),
                    Publisher = "J.B. Lippincott & Co.",
                    Description = "A novel about racial injustice in the American South.",
                    CoverImageUrl = "https://example.com/mockingbird.jpg",
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                },
                new Book
                {
                    GenreId = 3,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    ISBN = "9780743273565",
                    Price = 48,
                    Stock = 3,
                    PageCount = 180,
                    PublishedDate = new DateTime(1925, 4, 10),
                    Publisher = "Charles Scribner's Sons",
                    Description = "A story of wealth, love, and the American dream.",
                    CoverImageUrl = "https://example.com/gatsby.jpg",
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                }
            );
        }
    }
}
