using System.Data.Entity.ModelConfiguration;

namespace Dokan.Service.Product.EntityConfigurations
{
    public class ProductConfigurations : EntityTypeConfiguration<Models.Product>
    {
        public ProductConfigurations()
        {
            this.Property(s => s.ProductName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(s => s.ProductName)
                .IsConcurrencyToken();

            // Configure a one-to-one relationship between Student & StudentAddress
           // this.HasOptional(s => s.ProductDescription) // Mark Student.Address property optional (nullable)
            //    .WithRequired(ad => ad.Prod); // Mark StudentAddress.Student property as required (NotNull).
        }
    }
}
