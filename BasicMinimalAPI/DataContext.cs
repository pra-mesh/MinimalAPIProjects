global using Microsoft.EntityFrameworkCore;

namespace BasicMinimalAPI;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {

  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
    optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels; Database = studentdb;Trusted_Connection=true;TrustServerCertificate=true");
  }
  public DbSet<Student> Students => Set<Student>();
}
