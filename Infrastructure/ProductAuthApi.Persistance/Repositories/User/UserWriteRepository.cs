using ProductAuthApi.Application.Repositories;
using ProductAuthApi.Domain.Entities;
using ProductAuthApi.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Persistance.Repositories
{
	internal class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
	{
		public UserWriteRepository(ProductAuthApiDbContext context) : base(context)
		{
		}
	}
}
