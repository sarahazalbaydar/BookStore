namespace BookStore.Api.Features.Commands;

public class UpdateGenreCommand
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
