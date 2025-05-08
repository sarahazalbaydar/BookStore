namespace BookStore.Api.Features.Commands;

public class UpdateAuthorCommand
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
}
