using BookStore.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {

            // If database already contains data, not seed it again
            if (context.Books.Any() || context.Genres.Any() || context.Authors.Any())
                return;


            context.Genres.AddRange(
                new Genre { Name = "Dystopian", CreatedDate = DateTime.Now, IsActive = true },
                new Genre { Name = "Classic Literature", CreatedDate = DateTime.Now, IsActive = true },
                new Genre { Name = "Historical Fiction", CreatedDate = DateTime.Now, IsActive = true },
                new Genre { Name = "Science Fiction", CreatedDate = DateTime.Now, IsActive = true },
                new Genre { Name = "Mystery", CreatedDate = DateTime.Now, IsActive = true }
            );

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

            context.Authors.AddRange(
                new Author
                {
                    FirstName = "George",
                    LastName = "Orwell",
                    DateOfBirth = new DateTime(1903, 6, 25),
                    CreatedDate = DateTime.Now,
                    IsActive = true
                },
                new Author
                {
                    FirstName = "Harper",
                    LastName = "Lee",
                    DateOfBirth = new DateTime(1926, 4, 28),
                    CreatedDate = DateTime.Now,
                    IsActive = true
                },
                new Author
                {
                    FirstName = "F. Scott",
                    LastName = "Fitzgerald",
                    DateOfBirth = new DateTime(1896, 9, 24),
                    CreatedDate = DateTime.Now,
                    IsActive = true
                },
                new Author
                {
                    FirstName = "Jane",
                    LastName = "Austen",
                    DateOfBirth = new DateTime(1775, 12, 16),
                    CreatedDate = DateTime.Now,
                    IsActive = true
                },
                new Author
                {
                    FirstName = "Mark",
                    LastName = "Twain",
                    DateOfBirth = new DateTime(1835, 11, 30),
                    CreatedDate = DateTime.Now,
                    IsActive = true
                },
                new Author
                {
                    FirstName = "Agatha",
                    LastName = "Christie",
                    DateOfBirth = new DateTime(1890, 9, 15),
                    CreatedDate = DateTime.Now,
                    IsActive = true
                }
            );

            context.SaveChanges();
        }
    }
}
