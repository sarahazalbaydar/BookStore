namespace BookStore.Api.Features.Commands;

public class CreateAuthorCommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; } = true;
}
