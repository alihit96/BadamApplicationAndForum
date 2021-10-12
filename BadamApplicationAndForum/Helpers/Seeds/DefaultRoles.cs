using BadamApplicationAndForum.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Helpers.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<PanelUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Operator.ToString()));
        }
    }
}
