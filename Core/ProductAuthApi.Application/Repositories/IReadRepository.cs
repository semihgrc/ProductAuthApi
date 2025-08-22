using ProductApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.Repositories
{
	public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T?> GetByIdAsync(Guid id);
	}
}
