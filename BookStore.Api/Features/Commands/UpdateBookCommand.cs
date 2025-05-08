namespace BookStore.Api.Features.Commands;

public class UpdateBookCommand
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishedDate { get; set; }
    public string Publisher { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public bool IsActive { get; set; }
}
