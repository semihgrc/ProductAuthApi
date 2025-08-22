using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Persistance.Contexts
{
	public class ProductApiDbContext : DbContext
	{
		public ProductApiDbContext(DbContextOptions<ProductApiDbContext> options) : base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
	}
}
