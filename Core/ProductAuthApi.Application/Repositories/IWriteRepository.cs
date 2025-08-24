using ProductAuthApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Application.Repositories
{
	public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
	{
		Task<bool> AddAsync(T model);
		Task<bool> AddRangeAsync(List<T> datas);
		bool Remove(T model);
		Task<bool> RemoveAsync(Guid id);
		Task<bool> RemoveRangeAsync(List<T> datas);
		Task<bool> UpdateAsync(T model);
		Task<int> SaveAsync();
	}
}
