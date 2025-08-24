using ProductAuthApi.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAuthApi.Application.Services.Product
{
	public interface IProductService
	{
		Task<ProductDto> CreateAsync(CreateProductDto dto);
		Task<ProductDto> UpdateAsync(UpdateProductDto dto);
		Task<bool> DeleteAsync(Guid id);
		Task<ProductDto?> GetByIdAsync(Guid id);
		Task<IEnumerable<ProductViewDto>> GetAllAsync();
	}
}
