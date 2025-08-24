using ProductAuthApi.Application.Repositories;
using ProductAuthApi.Domain.Entities;
using ProductAuthApi.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Persistance.Repositories
{
	public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
	{
		public ProductWriteRepository(ProductAuthApiDbContext context) : base(context)
		{

		}
	}
}
