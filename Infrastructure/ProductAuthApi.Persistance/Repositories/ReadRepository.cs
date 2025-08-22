using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Repositories;
using ProductApi.Domain.Entities.Common;
using ProductApi.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Persistance.Repositories
{
	public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
	{
		private readonly ProductApiDbContext _context;

		public ReadRepository(ProductApiDbContext context)
		{
			_context = context;
		}

		public DbSet<T> Table => _context.Set<T>();

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await Table.ToListAsync();
		}

		public async Task<T?> GetByIdAsync(Guid id)
		{
			return await Table.FindAsync(id);
		}
	}
}
