using Domain.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Seed;

public class RoleSeed
{
    public static async Task SeedUsers(RoleManager<Role> roleManager)
    {
        if (await roleManager.Roles.AnyAsync()) return;


        var roles = new List<Role>
            {
                new() { Name = "Customer" },
                new() { Name = "Admin" },
                new() { Name = "Moderator" }
            };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
    }
}
