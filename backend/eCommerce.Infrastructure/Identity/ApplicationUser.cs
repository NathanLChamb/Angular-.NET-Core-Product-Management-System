using Microsoft.AspNetCore.Identity;

namespace eCommerce.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public required string DisplayName { get; set; }
}