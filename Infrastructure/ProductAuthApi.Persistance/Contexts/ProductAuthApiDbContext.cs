using Microsoft.EntityFrameworkCore;
using ProductAuthApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Persistance.Contexts
{
	public class ProductAuthApiDbContext : DbContext
	{
		public ProductAuthApiDbContext(DbContextOptions<ProductAuthApiDbContext> options) : base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
