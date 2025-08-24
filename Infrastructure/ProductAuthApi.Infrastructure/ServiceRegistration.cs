using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductAuthApi.Application.Services.Auth;
using ProductAuthApi.Application.Services.Product;
using ProductAuthApi.Application.Services.Token;
using ProductAuthApi.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Infrastructure
{
	public static class ServiceRegistration
	{
		public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<ITokenService, TokenService>();
		}
	}
}
