using IMS.Application.Interfaces;
using IMS.Domain.Entities;
using IMS.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.Repositories
{
	public class OrderService : IOrderService
	{
		private readonly AppDbContext _context;

		public OrderService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<int> CreateOrderAsync(Order order)
		{
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();
			return order.Id;
		}

		public async Task<Order?> GetOrderByIdAsync(int id)
		{
			return await _context.Orders
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.Product)
				.FirstOrDefaultAsync(o => o.Id == id);
		}
	}
}
