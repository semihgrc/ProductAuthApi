using Microsoft.EntityFrameworkCore;
using ProductAuthApi.Application.Repositories;
using ProductAuthApi.Domain.Entities.Common;
using ProductAuthApi.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Persistance.Repositories
{
	public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
	{
		private readonly ProductAuthApiDbContext _context;

		public ReadRepository(ProductAuthApiDbContext context)
		{
			_context = context;
		}

		public DbSet<T> Table => _context.Set<T>();

		public IQueryable<T> GetAll()
		{
			return Table;
		}

		public async Task<T> GetByIdAsync(Guid id) 
		{
			return await Table.FindAsync(id);
		}

		public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
		{
			return await Table.FirstOrDefaultAsync(predicate);
		}

		public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
		{
			return Table.Where(predicate);
		}
	}
}
