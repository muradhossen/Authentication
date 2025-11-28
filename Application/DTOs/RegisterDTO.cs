using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class RegisterDTO
{
    [Required] public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required] public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
    public string Address { get; set; }
    public string PhotoUrl { get; set; }

    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; }
}
