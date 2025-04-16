using AVBlog.Domain.Constants;
using AVBlog.Domain.Entities.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AVBlog.Infrastructure.Data
{
    public static class DataSeeding
    {
        public static async Task SeedData(this IApplicationBuilder app)
        {
            await CreateDefaultUserAdmin(app);
        }

        private static async Task CreateDefaultUserAdmin(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var serviceProvider = scope.ServiceProvider;

            var context = serviceProvider.GetRequiredService<AVBlogCommandContext>();
            await context.Database.MigrateAsync();

            await AdDefaultRoleAsync();
            await AddDefaultUserAdminAsync();

            async Task AdDefaultRoleAsync()
            {
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
                if (roleManager != null)
                {
                    var defaultRoles = new[] { RoleConstant.Admin, RoleConstant.User };
                    foreach (var role in defaultRoles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }
                }
            }

            async Task AddDefaultUserAdminAsync()
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
                if (userManager != null)
                {
                    var adminUsers = await userManager.GetUsersInRoleAsync(RoleConstant.Admin);
                    if (!adminUsers.Any())
                    {
                        var adminName = "Admin";
                        var defaultPassword = "Abc@123";

                        var adminUser = new AppUser
                        {
                            UserName = adminName,
                            EmailConfirmed = true,
                        };

                        var identityResult = await userManager.CreateAsync(adminUser, defaultPassword);
                        if (identityResult.Succeeded)
                        {
                            await userManager.AddToRoleAsync(adminUser, RoleConstant.Admin);
                        }
                    }
                }
            }
        }


    }
}
