using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Dokan.Service.Customer.Models
{
    public class CustomerDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public CustomerDbContext(IConfiguration configuration,DbContextOptions<CustomerDbContext> dbContextOptions): base()
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
