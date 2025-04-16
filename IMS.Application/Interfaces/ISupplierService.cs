﻿using IMS.Domain.Entities;

namespace IMS.Application.Interfaces
{
	public interface ISupplierService
	{
		Task<IEnumerable<Supplier>> GetAllAsync();
		Task<Supplier?> GetByIdAsync(int id);
		Task AddAsync(Supplier supplier);
		Task UpdateAsync(Supplier supplier);
		Task DeleteAsync(int id);
	}
}
