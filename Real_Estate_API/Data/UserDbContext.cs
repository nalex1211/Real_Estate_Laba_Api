using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Real_Estate_API.Models;

namespace Real_Estate_API.Data;

public class UserDbContext : IdentityDbContext<Agent>
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }
}