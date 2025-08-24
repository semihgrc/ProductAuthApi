using Microsoft.AspNetCore.Mvc;
using ProductAuthApi.Application.DTOs.Product;
using ProductAuthApi.Application.Services.Product;

namespace ProductApi.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		/// <summary>
		/// Yeni bir ürün oluşturur.
		/// </summary>
		/// <param name="dto">Oluşturulacak ürün bilgisi.</param>
		/// <returns>Oluşturulan ürün.</returns>
		/// <response code="201">Ürün başarıyla oluşturuldu.</response>
		/// <response code="400">Geçersiz ürün bilgisi gönderildi.</response>
		[HttpPost]
		[ProducesResponseType(typeof(ProductDto), 201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
		{
			try
			{
				var createdProduct = await _productService.CreateAsync(dto);
				return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Message = "Ürün oluşturulurken bir hata oluştu.", Details = ex.Message });
			}
		}

		/// <summary>
		/// Tüm ürünleri listeler.
		/// </summary>
		/// <returns>Ürün listesi.</returns>
		/// <response code="200">Ürünler başarıyla getirildi.</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var products = await _productService.GetAllAsync();
				return Ok(products);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Message = "Ürünler listelenirken bir hata oluştu.", Details = ex.Message });
			}
		}

		/// <summary>
		/// Belirtilen ID'ye sahip ürünü getirir.
		/// </summary>
		/// <param name="id">Ürün ID'si.</param>
		/// <returns>Ürün bilgisi.</returns>
		/// <response code="200">Ürün bulundu.</response>
		/// <response code="404">Ürün bulunamadı.</response>
		[HttpGet("{id:guid}")]
		[ProducesResponseType(typeof(ProductDto), 200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetById(Guid id)
		{
			try
			{
				var product = await _productService.GetByIdAsync(id);
				if (product == null) return NotFound();
				return Ok(product);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Message = "Ürün getirilirken bir hata oluştu.", Details = ex.Message });
			}
		}

		/// <summary>
		/// Var olan bir ürünü günceller.
		/// </summary>
		/// <param name="id">Ürün ID'si.</param>
		/// <param name="dto">Güncellenecek ürün bilgisi.</param>
		/// <returns>Güncellenmiş ürün.</returns>
		/// <response code="200">Ürün başarıyla güncellendi.</response>
		/// <response code="400">Gönderilen ID ile DTO ID uyuşmadı.</response>
		[HttpPut("{id:guid}")]
		[ProducesResponseType(typeof(ProductDto), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
		{
			try
			{
				if (id != dto.Id) return BadRequest("ID mismatch");
				var updatedProduct = await _productService.UpdateAsync(dto);
				return Ok(updatedProduct);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Message = "Ürün güncellenirken bir hata oluştu.", Details = ex.Message });
			}
		}

		/// <summary>
		/// Var olan bir ürünü siler.
		/// </summary>
		/// <param name="id">Silinecek ürünün ID'si.</param>
		/// <response code="204">Ürün başarıyla silindi.</response>
		/// <response code="404">Ürün bulunamadı.</response>
		[HttpDelete("{id:guid}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				var deleted = await _productService.DeleteAsync(id);
				if (!deleted) return NotFound();
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Message = "Ürün silinirken bir hata oluştu.", Details = ex.Message });
			}
		}
	}
}
