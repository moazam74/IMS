using IMS.Application.Interfaces;
using IMS.Domain.Entities;
using IMS.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.Repositories
{
	public class CategoryService : ICategoryService
	{
		private readonly AppDbContext _context;

		public CategoryService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.ToListAsync();

		public async Task<Category?> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);

		public async Task AddAsync(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Category category)
		{
			_context.Categories.Update(category);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category != null)
			{
				_context.Categories.Remove(category);
				await _context.SaveChangesAsync();
			}
		}
	}
}
