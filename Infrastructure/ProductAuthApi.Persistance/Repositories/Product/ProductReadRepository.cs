using ProductApi.Application.Repositories;
using ProductApi.Domain.Entities;
using ProductApi.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Persistance.Repositories
{
	public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
	{
		public ProductReadRepository(ProductApiDbContext context) : base(context)
		{
		}
	}
}
