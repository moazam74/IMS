namespace IMS.Application.DTOs
{
	public class OrderDetailDto : OrderDto
	{
		public List<OrderItemDto> Items { get; set; } = new();
	}
}
