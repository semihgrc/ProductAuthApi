using ProductAuthApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Application.Repositories
{
	public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
	{

		IQueryable<T> GetAll();
		Task<T> GetByIdAsync(Guid id); 
		Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
		IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
	}
}
