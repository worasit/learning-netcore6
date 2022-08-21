using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Identity.DbContexts;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
}