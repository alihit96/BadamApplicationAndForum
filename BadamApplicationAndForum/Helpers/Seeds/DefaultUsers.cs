using BadamApplicationAndForum.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Helpers.Seeds
{

    public static class DefaultUsers
    {
        public static async Task SeedOperatorAsync(UserManager<PanelUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new PanelUser
            {
                UserName = "operator",
                Email = "operator@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123P@ssw0rd");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Operator.ToString());
                }
            }
        }
        public static async Task SeedAdminAsync(UserManager<PanelUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new PanelUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Operator.ToString());
                }
                await roleManager.SeedClaimsForAdmin();
            }
        }
        private async static Task SeedClaimsForAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            await roleManager.AddPermissionClaim(adminRole, "کاربران");
            await roleManager.AddPermissionClaim(adminRole, "بنر");
            await roleManager.AddPermissionClaim(adminRole, "اعلان");
            await roleManager.AddPermissionClaim(adminRole, "اخبار");
            await roleManager.AddPermissionClaim(adminRole, "پرسنل");
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }

}
