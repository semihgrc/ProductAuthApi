using ProductApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.Repositories
{
	public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
	{
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
	}
}
