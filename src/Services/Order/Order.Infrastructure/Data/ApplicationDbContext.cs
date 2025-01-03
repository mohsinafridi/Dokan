
// using Order.Application.Data;
using Order.Application.Data;
using System.Reflection;

namespace Order.Infrastructure.Data;

public class ApplicationDbContext : DbContext , IApplicationDbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options) { }
    //public ApplicationDbContext()
    //{

    //}
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Domain.Models.Order> Orders => Set<Domain.Models.Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{

    //    optionsBuilder.UseSqlServer("Server=localhost;Database=OrderDb;User Id=sa;Password=SwN12345678;Encrypt=False;TrustServerCertificate=True");

    //}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
