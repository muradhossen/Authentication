using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Account;

public class User : IdentityUser<long>
{
    [Required(ErrorMessage = $"The {nameof(FirstName)} is required.")]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public string PhotoUrl { get; set; }
    public string PhotoPublicId { get; set; }
    public DateTime LastActive { get; set; } = DateTime.Now;
    public DateTime Created { get; set; } = DateTime.Now;
    public ICollection<UserRole> UserRoles { get; set; }
}
