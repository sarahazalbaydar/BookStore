namespace BookStore.Api.Features.Models;

public class AuthorResponseModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
}
