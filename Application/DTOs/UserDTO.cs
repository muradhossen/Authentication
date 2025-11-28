using Domain.Entities.Account;

namespace Application.DTOs;

public class UserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
    public string Gender { get; set; }
    public string Address { get; set; }
    public DateTime LastActive { get; set; }
    public DateTime Created { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Token { get; set; }
    public string PhotoUrl { get; set; }
}
