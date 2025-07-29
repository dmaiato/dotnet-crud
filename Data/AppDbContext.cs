using Microsoft.EntityFrameworkCore;
using SimpleAPI.Students;

namespace SimpleAPI.Data;

public class AppDbContext : DbContext
{
  public DbSet<Student> Students { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=Database.sqlite");
    optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

    // enables param data to be shown on console logging
    // only use while testing.
    // optionsBuilder.EnableSensitiveDataLogging();

    base.OnConfiguring(optionsBuilder);
  }
}