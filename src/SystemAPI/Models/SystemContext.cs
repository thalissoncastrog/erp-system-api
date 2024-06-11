using Microsoft.EntityFrameworkCore;

namespace SystemAPI.Models;

public class SystemContext : DbContext
{
    public SystemContext(DbContextOptions<SystemContext> options)
        : base(options)
    {}
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<City> Cities { get; set; }
}