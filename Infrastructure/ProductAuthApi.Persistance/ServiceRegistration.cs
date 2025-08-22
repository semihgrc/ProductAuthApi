using Microsoft.Extensions.DependencyInjection;
using ProductApi.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Repositories;
using ProductApi.Persistance.Repositories;

namespace ProductApi.Persistance
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceServices(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<ProductApiDbContext>(options =>
				options.UseNpgsql(connectionString));

			services.AddScoped<IProductReadRepository, ProductReadRepository>();
			services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
		}
	}
}