using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPMVC.Data.Migrations
{
    public class ApplicationDbInit
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationDbInit(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async void Seed()
        {
            if ((await _roleManager.FindByNameAsync("Administrator")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Administrator" });

            }
            if ((await _roleManager.FindByNameAsync("User")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });

            }
            if ((await _userManager.FindByEmailAsync("admin@asp.net")) == null)
            {
                var admin = new IdentityUser
                {
                    UserName = "admin@asp.net",
                    Email = "admin@asp.net"
                };

                var result = await _userManager.CreateAsync(admin);
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(admin, "Zaq123$%^");
                    await _userManager.AddToRoleAsync(admin, "Administrator");

                }
            }
            if ((await _userManager.FindByEmailAsync("user@asp.net")) == null)
            {
                var user = new IdentityUser
                {
                    UserName = "user@asp.net",
                    Email = "user@asp.net"
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, "Zaq123$%^");
                    await _userManager.AddToRoleAsync(user, "User");

                }
            }
        }
    }
}
