using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.DTOs.Product
{
	public class UpdateProductDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public string? Category { get; set; }
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}
}
