﻿using IMS.Application.Interfaces;
using IMS.Domain.Entities;
using IMS.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.Repositories
{
	public class ProductService : IProductService
	{
		private readonly AppDbContext _context;

		public ProductService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await _context.Products
				.Include(p => p.Category)
				.Include(p => p.Supplier)
				.ToListAsync();
		}

		public async Task<Product?> GetByIdAsync(int id)
		{
			return await _context.Products
				.Include(p => p.Category)
				.Include(p => p.Supplier)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task AddAsync(Product product)
		{
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Product product)
		{
			_context.Products.Update(product);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product != null)
			{
				_context.Products.Remove(product);
				await _context.SaveChangesAsync();
			}
		}
	}
}
