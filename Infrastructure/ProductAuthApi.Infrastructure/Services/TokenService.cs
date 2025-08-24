﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductAuthApi.Application.Services.Token;
using ProductAuthApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Infrastructure.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _config;

		public TokenService(IConfiguration config)
		{
			_config = config;
		}
		public string GenerateToken(User user)
		{
			var claims = new[]
			{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.FullName),
			new Claim(ClaimTypes.Email, user.Email)

		};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
