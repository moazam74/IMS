using IMS.Domain.Entities;

namespace IMS.Application.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetAllAsync();
		Task<Product?> GetByIdAsync(int id);
		Task AddAsync(Product product);
		Task UpdateAsync(Product product);
		Task DeleteAsync(int id);
	}
}
