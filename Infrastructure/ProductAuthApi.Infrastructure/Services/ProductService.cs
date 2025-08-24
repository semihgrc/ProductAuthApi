using ProductAuthApi.Application.DTOs.Product;
using ProductAuthApi.Application.Repositories;
using ProductAuthApi.Application.Services.Product;
using ProductAuthApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAuthApi.Infrastructure.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductReadRepository _readRepository;
		private readonly IProductWriteRepository _writeRepository;

		public ProductService(IProductReadRepository readRepository, IProductWriteRepository writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public async Task<ProductDto> CreateAsync(CreateProductDto dto)
		{
			var product = new Product
			{
				Name = dto.Name,
				Description = dto.Description,
				Price = dto.Price,
				Category = dto.Category,
				CreatedAt = DateTime.UtcNow
			};

			var added = await _writeRepository.AddAsync(product);
			await _writeRepository.SaveAsync(); 

			return new ProductDto
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				Category = product.Category,
				CreatedAt = product.CreatedAt,
				UpdatedAt = product.UpdatedAt
			};
		}

		public async Task<ProductDto> UpdateAsync(UpdateProductDto dto)
		{
			var product = await _readRepository.GetByIdAsync(dto.Id);
			if (product == null) throw new KeyNotFoundException("Product not found");

			product.Name = dto.Name;
			product.Description = dto.Description;
			product.Price = dto.Price;
			product.Category = dto.Category;
			product.UpdatedAt = DateTime.UtcNow;

			await _writeRepository.UpdateAsync(product);
			await _writeRepository.SaveAsync();

			return new ProductDto
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				Category = product.Category,
				CreatedAt = product.CreatedAt,
				UpdatedAt = product.UpdatedAt
			};
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var success = await _writeRepository.RemoveAsync(id);
			if (!success) return false;

			await _writeRepository.SaveAsync();
			return true;
		}
		public async Task<ProductDto?> GetByIdAsync(Guid id)
		{
			var product = await _readRepository.GetByIdAsync(id);
			if (product == null) return null;

			return new ProductDto
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				Category = product.Category,
				CreatedAt = product.CreatedAt,
				UpdatedAt = product.UpdatedAt
			};
		}

		public Task<IEnumerable<ProductViewDto>> GetAllAsync()
		{
			var products = _readRepository.GetAll().ToList();

			var result = products.Select(p => new ProductViewDto
			{
				Name = p.Name,
				Description = p.Description,
				Price = p.Price,
				Category = p.Category
			});

			return Task.FromResult(result);
		}
	}
}
