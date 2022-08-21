using System.Security.Claims;
using IdentityModel;
using Mango.Services.Identity.DbContexts;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Identity.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<DbInitializer> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public void Initialize()
    {
        if (_roleManager.FindByNameAsync(SD.Roles.Admin).Result != null)
        {
            _logger.LogInformation("Users and Roles are already existed.");
            return;
        }

        _logger.LogInformation("Initialize all Users and Roles");
        _roleManager.CreateAsync(new IdentityRole(SD.Roles.Admin)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(SD.Roles.Customer)).GetAwaiter().GetResult();

        var adminUser = new ApplicationUser
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "0970030200",
            FirstName = "worasit",
            Lastname = "daimongkol",
            Age = 32
        };
        var customerUser = new ApplicationUser
        {
            UserName = "customer@gmail.com",
            Email = "customer@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "0805866459",
            FirstName = "yumi",
            Lastname = "sugimoto",
            Age = 28
        };
        BuildUser(adminUser, SD.Roles.Admin, "Admin@123");
        BuildUser(customerUser, SD.Roles.Customer, "Customer@123");

        _logger.LogInformation("Completed Roles & Users initialization");
    }

    private void BuildUser(ApplicationUser applicationUser, string role, string password)
    {
        _userManager.CreateAsync(applicationUser, password).GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(applicationUser, role).GetAwaiter().GetResult();
        _ = _userManager.AddClaimsAsync(applicationUser, new[]
        {
            new Claim(JwtClaimTypes.Name, applicationUser.FirstName + " " + applicationUser.Lastname),
            new Claim(JwtClaimTypes.GivenName, applicationUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, applicationUser.Lastname),
            new Claim(JwtClaimTypes.Email, applicationUser.Email),
            new Claim(JwtClaimTypes.Role, role)
        }).Result;
    }
}