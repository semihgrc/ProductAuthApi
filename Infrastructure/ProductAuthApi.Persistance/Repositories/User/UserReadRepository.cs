using ProductAuthApi.Domain.Entities;
using ProductAuthApi.Application.Repositories;
using ProductAuthApi.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Persistance.Repositories
{
	public class UserReadRepository : ReadRepository<User>, IUserReadRepository
	{
		public UserReadRepository(ProductAuthApiDbContext context) : base(context)
		{
		}
	}
}
