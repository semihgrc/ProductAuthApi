using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAuthApi.Application.DTOs.User;
using ProductAuthApi.Application.Repositories;
using ProductAuthApi.Application.Services.Auth;
using System.Security.Claims;

namespace ProductAuthApi.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")] // Swagger için response tipi
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly IUserReadRepository _userReadRepository;

		public AuthController(IAuthService authService, IUserReadRepository userReadRepository)
		{
			_authService = authService;
			_userReadRepository = userReadRepository;
		}

		/// <summary>
		/// Yeni kullanıcı kaydı oluşturur ve JWT token döner.
		/// </summary>
		/// <param name="dto">Register bilgileri</param>
		/// <returns>Access token</returns>
		/// <response code="200">Kullanıcı başarıyla oluşturuldu</response>
		/// <response code="400">Kayıt sırasında bir hata oluştu</response>
		[HttpPost("register")]
		[ProducesResponseType(typeof(object), 200)]
		[ProducesResponseType(typeof(object), 400)]
		public async Task<IActionResult> Register([FromBody] RegisterDto dto)
		{
			try
			{
				var token = await _authService.RegisterAsync(dto);
				return Ok(new { access_token = token });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Kullanıcı girişi yapar ve JWT token döner.
		/// </summary>
		/// <param name="dto">Login bilgileri</param>
		/// <returns>Access token</returns>
		/// <response code="200">Giriş başarılı</response>
		/// <response code="401">Giriş bilgileri hatalı</response>
		[HttpPost("login")]
		[ProducesResponseType(typeof(object), 200)]
		[ProducesResponseType(typeof(object), 401)]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			try
			{
				var token = await _authService.LoginAsync(dto);
				return Ok(new { access_token = token });
			}
			catch (Exception ex)
			{
				return Unauthorized(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Giriş yapmış Mevcut kullanıcı bilgilerini getirir.
		/// </summary>
		/// <returns>Kullanıcı bilgileri</returns>
		/// <response code="200">Kullanıcı bulundu</response>
		/// <response code="401">Token geçersiz</response>
		/// <response code="404">Kullanıcı bulunamadı</response>
		[Authorize]
		[HttpGet("me")]
		[ProducesResponseType(typeof(object), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Me()
		{
			var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (!Guid.TryParse(userIdClaim, out var userId))
				return Unauthorized("Invalid user ID in token");

			var user = await _userReadRepository.GetByIdAsync(userId);
			if (user == null)
				return NotFound("User not found");

			return Ok(new
			{
				id = user.Id,
				fullName = user.FullName,
				email = user.Email
			});
		}
	}
}
