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
	public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
	{
		public ProductWriteRepository(ProductApiDbContext context) : base(context)
		{

		}
	}
}
