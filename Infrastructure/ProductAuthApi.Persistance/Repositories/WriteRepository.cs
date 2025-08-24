using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductAuthApi.Application.Repositories;
using ProductAuthApi.Domain.Entities.Common;
using ProductAuthApi.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Persistance.Repositories
{
	public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
	{
		private readonly ProductAuthApiDbContext _context;

		public WriteRepository(ProductAuthApiDbContext context)
		{
			_context = context;
		}

		public DbSet<T> Table => _context.Set<T>();

		public async Task<bool> AddAsync(T model)
		{
			EntityEntry<T> entityEntry = await Table.AddAsync(model);
			return entityEntry.State == EntityState.Added;
		}

		public async Task<bool> AddRangeAsync(List<T> datas)
		{
			await Table.AddRangeAsync(datas);
			return true;
		}

		public bool Remove(T model)
		{
			EntityEntry<T> entityEntry = Table.Remove(model);
			return entityEntry.State == EntityState.Deleted;
		}

		public async Task<bool> RemoveAsync(Guid id) 
		{
			T model = await Table.FirstOrDefaultAsync(data => data.Id == id);
			if (model == null)
				return false;

			return Remove(model);
		}

		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public Task<bool> RemoveRangeAsync(List<T> datas)
		{
			Table.RemoveRange(datas);
			return Task.FromResult(true);
		}

		public Task<bool> UpdateAsync(T model)
		{
			EntityEntry entityEntry = Table.Update(model);
			return Task.FromResult(entityEntry.State == EntityState.Modified);
		}
	}

}

