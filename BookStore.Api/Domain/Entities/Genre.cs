using BookStore.Base.Domain;

namespace BookStore.Api.Domain.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Book> Books { get; set; }
}
