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
        // Configuração da entidade City
        modelBuilder.Entity<City>()
            .HasKey(city => city.City_Id);

        modelBuilder.Entity<City>()
            .Property(city => city.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<City>()
            .Property(city => city.State)
            .IsRequired()
            .HasMaxLength(50);

        // Configuração da entidade Client
        modelBuilder.Entity<Client>()
            .HasKey(client => client.ClientId);

        modelBuilder.Entity<Client>()
            .Property(client => client.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Client>()
            .Property(client => client.Gender)
            .IsRequired()
            .HasMaxLength(1);

        modelBuilder.Entity<Client>()
            .Property(client => client.BirthDate)
            .IsRequired();

        modelBuilder.Entity<Client>()
            .Property(client => client.Age)
            .IsRequired();

        // Relacionamento entre Client e City
        modelBuilder.Entity<Client>()
            .HasOne(client => client.City)
            .WithMany(city => city.Clients)
            .HasForeignKey(client => client.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}