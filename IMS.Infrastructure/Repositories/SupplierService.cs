using IMS.Application.Interfaces;
using IMS.Domain.Entities;
using IMS.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.Repositories
{
	public class SupplierService : ISupplierService
	{
		private readonly AppDbContext _context;

		public SupplierService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Supplier>> GetAllAsync() => await _context.Suppliers.ToListAsync();

		public async Task<Supplier?> GetByIdAsync(int id) => await _context.Suppliers.FindAsync(id);

		public async Task AddAsync(Supplier supplier)
		{
			_context.Suppliers.Add(supplier);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Supplier supplier)
		{
			_context.Suppliers.Update(supplier);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var supplier = await _context.Suppliers.FindAsync(id);
			if (supplier != null)
			{
				_context.Suppliers.Remove(supplier);
				await _context.SaveChangesAsync();
			}
		}
	}
}
