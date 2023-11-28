namespace KeyAuth;

using Microsoft.EntityFrameworkCore;

public class AppContext : DbContext
{
    public DbSet<KeyInfo> Keys { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=keys;username=postgres;Password=1234;Include Error Detail=True");
        base.OnConfiguring(optionsBuilder);
    }
}