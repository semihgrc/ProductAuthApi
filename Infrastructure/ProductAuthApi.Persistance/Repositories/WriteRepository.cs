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
	public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
	{

		private readonly ProductApiDbContext _context;

		public WriteRepository(ProductApiDbContext context)
		{
			_context = context;
		}
		public DbSet<T> Table => _context.Set<T>();

		public async Task<T> AddAsync(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			await Table.AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			Table.Remove(entity);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			Table.Update(entity);
			await _context.SaveChangesAsync();
		}
	}

}

