using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProductAuthApi.Persistance.Contexts;

namespace ProductAuthApi.Persistance
{
	public class ProductApiDbContextFactory : IDesignTimeDbContextFactory<ProductAuthApiDbContext>
	{
		public ProductAuthApiDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ProductAuthApiDbContext>();
			optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=ProductAuthDb;Username=postgres;Password=semsam123");

			return new ProductAuthApiDbContext(optionsBuilder.Options);
		}
	}
}
