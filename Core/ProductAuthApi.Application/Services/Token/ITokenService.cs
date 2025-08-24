using ProductAuthApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Application.Services.Token
{
	public interface ITokenService
	{
		string GenerateToken(User user);
	}
}
