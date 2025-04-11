using IMS.Domain.Entities;

namespace IMS.Application.Interfaces
{
	public interface IOrderService
	{
		Task<int> CreateOrderAsync(Order order); // returns OrderId
		Task<Order?> GetOrderByIdAsync(int id);
	}
}
