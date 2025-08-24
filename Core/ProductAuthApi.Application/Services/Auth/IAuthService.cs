using ProductAuthApi.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Application.Services.Auth
{
	public interface IAuthService
	{
		Task<string> RegisterAsync(RegisterDto dto);
		Task<string> LoginAsync(LoginDto dto);
	}
}
