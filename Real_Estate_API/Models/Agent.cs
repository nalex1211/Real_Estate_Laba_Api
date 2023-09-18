using Microsoft.AspNetCore.Identity;

namespace Real_Estate_API.Models;

public class Agent : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
