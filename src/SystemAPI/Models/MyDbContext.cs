using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SystemAPI.Models.Entities;
using DotNetEnv;

public class MyDbContext : DbContext
{
    private IConfiguration _configuration;

    public DbSet<City> Cities { get; set; }
    public DbSet<Client> Clients { get; set; }

    public MyDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        Env.Load();

        var typeDataBase = _configuration["TypeDataBase"];
        var connectionString = _configuration.GetSection($"ConnectionStrings:{typeDataBase}").Value;

        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseUrl))
        {
            throw new ArgumentException("The environment variable is not defined");
        }

        connectionString = connectionString.Replace("${DATABASE_URL}", databaseUrl);

        if (typeDataBase == "SqlServer")
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>()
            .HasMany(c => c.Clients)
            .WithOne(e => e.City)
            .HasForeignKey(e => e.CityId);
    }
}