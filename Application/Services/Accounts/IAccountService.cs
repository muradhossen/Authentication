using Domain.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Accounts;

public interface IAccountService
{
    Task<bool> IsUserExist(string userName);
    Task<User> GetUserByUsername(string username);
}

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
  

    public AccountService(UserManager<User> userManager)
    {
        _userManager = userManager; 
    }
    public async Task<bool> IsUserExist(string userName)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == userName.ToLower()); 
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _userManager
                    .Users
                    .SingleOrDefaultAsync(c => c.UserName.ToLower() == username.ToLower()); 
    }
}
