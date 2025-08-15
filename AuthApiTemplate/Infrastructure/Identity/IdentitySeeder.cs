using Microsoft.AspNetCore.Identity;

namespace AuthApiTemplate.Infrastructure.Identity;

public static class IdentitySeeder
{
    public static readonly string[] RoleNames = ["Admin", "User"];


    public static async Task InitializeRoles(IServiceProvider serviceProvider, bool throwOnError = true)
    {
        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();
        RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (string roleName in RoleNames)
        {
            bool roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(roleName));

                if (throwOnError && !result.Succeeded)
                {
                    bool isDuplicate = result.Errors.Any(e => e.Code.Contains("DuplicateRoleName", StringComparison.OrdinalIgnoreCase));

                    if (!isDuplicate)
                    {
                        string details = string.Join("; ", result.Errors.Select(e => $"[{e.Code}] {e.Description}"));
                        string message = $"Failed to create role {roleName}: {details}";
                        throw new InvalidOperationException(message);
                    }
                }
            }
        } 
    }
}
