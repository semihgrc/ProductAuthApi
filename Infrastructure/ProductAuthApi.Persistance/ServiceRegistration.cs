using Microsoft.Extensions.DependencyInjection;
using ProductAuthApi.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using ProductAuthApi.Application.Repositories;
using ProductAuthApi.Persistance.Repositories;

namespace ProductAuthApi.Persistance
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceServices(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<ProductAuthApiDbContext>(options =>
				options.UseNpgsql(connectionString));

			services.AddScoped<IProductReadRepository, ProductReadRepository>();
			services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
			services.AddScoped<IUserWriteRepository, UserWriteRepository>();
			services.AddScoped<IUserReadRepository,UserReadRepository>();	
		}
	}
}