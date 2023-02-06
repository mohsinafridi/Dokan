using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dokan.Service.Customer.Models
{
    public class CustomerDbContext : DbContext
    {
         protected readonly IConfiguration Configuration;
        private readonly string connectionString = "Server=MOHSIN\\SQLEXPRESS;Database=CustomerDb;User ID=sa;Password=";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          //  optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
//            builder.Entity<Customer>().HasQueryFilter(x => x);
        }
        public CustomerDbContext(IConfiguration configuration, DbContextOptions<CustomerDbContext> dbContextOptions) : base()
        {
            Configuration = configuration;
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public DbSet<Customer> Customers { get; set;}
    }
}
