using Microsoft.EntityFrameworkCore;
using SimpleAPI.Students;

namespace SimpleAPI.Data;

public class AppDbContext : DbContext
{
  private DbSet<Student> Students { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=Database.sqlite");
    base.OnConfiguring(optionsBuilder);
  }
}