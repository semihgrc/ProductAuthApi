using ProductAuthApi.Application.DTOs.User;
using ProductAuthApi.Application.Repositories;
using ProductAuthApi.Application.Services.Auth;
using ProductAuthApi.Application.Services.Token;
using ProductAuthApi.Domain.Entities;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Infrastructure.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserReadRepository _userReadRepository;
		private readonly IUserWriteRepository _userWriteRepository;
		private readonly ITokenService _tokenService;

		public AuthService(
			IUserReadRepository userReadRepository,
			IUserWriteRepository userWriteRepository,
			ITokenService tokenService)
		{
			_userReadRepository = userReadRepository;
			_userWriteRepository = userWriteRepository;
			_tokenService = tokenService;
		}

		public async Task<string> RegisterAsync(RegisterDto dto)
		{

			var existingEmail = await _userReadRepository.GetSingleAsync(u => u.Email == dto.Email);
			if (existingEmail != null)
				throw new Exception("İsim alınmış");

			var existingName = await _userReadRepository.GetSingleAsync(u => u.FullName == dto.FullName);
			if (existingName != null)
				throw new Exception("İsim daha önce alınmış");

			var user = new User
			{
				Id = Guid.NewGuid(),
				FullName = dto.FullName,
				Email = dto.Email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
				CreatedAt = DateTime.UtcNow,
			};

			await _userWriteRepository.AddAsync(user);
			await _userWriteRepository.SaveAsync();

			return _tokenService.GenerateToken(user);
		}


		public async Task<string> LoginAsync(LoginDto dto)
		{
			var user = await _userReadRepository.GetSingleAsync(u => u.Email == dto.Email);
			if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
				throw new Exception("Invalid credentials");

			return _tokenService.GenerateToken(user);
		}

	}
}
