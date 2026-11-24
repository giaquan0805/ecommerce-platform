using ecommerce_platform.Common.Constants;
using ecommerce_platform.Configurations;
using ecommerce_platform.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ecommerce_platform.Data.Seed
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles =
            {
                RoleConstants.Admin,
                RoleConstants.Seller,
                RoleConstants.Customer
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));

                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));

                        throw new InvalidOperationException($"Failed to create role '{role}': {errors}");
                    }
                }
            }
        }

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, AdminSettings adminSettings)
        {
            // Validate configuration
            if (string.IsNullOrWhiteSpace(adminSettings.Email) ||
                string.IsNullOrWhiteSpace(adminSettings.UserName))
            {
                throw new InvalidOperationException("Admin configuration is missing.");
            }

            // Check existing user
            var existingUser = await userManager.FindByEmailAsync(adminSettings.Email);
            if (existingUser != null)
            {
                if (await userManager.IsInRoleAsync(existingUser, RoleConstants.Admin))
                {
                    return;
                }

                throw new InvalidOperationException("Admin email is already used by another account.");
            }

            var existingUserName = await userManager.FindByNameAsync(adminSettings.UserName);
            if (existingUserName != null)
            {
                throw new InvalidOperationException("Admin username is already used by another account.");
            }

            // Validate password
            if (string.IsNullOrWhiteSpace(adminSettings.Password))
            {
                throw new InvalidOperationException("Admin password is missing.");
            }

            // Create Admin user
            var admin = new ApplicationUser
            {
                UserName = adminSettings.UserName,
                Email = adminSettings.Email,
                FullName = adminSettings.FullName,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var createResult = await userManager.CreateAsync(admin, adminSettings.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));

                throw new InvalidOperationException($"Failed to create admin account: {errors}");
            }

            // Assign Admin role
            var roleResult = await userManager.AddToRoleAsync(admin, RoleConstants.Admin);
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));

                throw new InvalidOperationException($"Failed to assign admin role: {errors}");
            }
        }
    }
}
