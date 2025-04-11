namespace IMS.Domain.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public decimal TotalAmount { get; set; }
		public DateTime Date { get; set; }
		public string StripePaymentId { get; set; } = string.Empty;

		public ICollection<OrderItem>? OrderItems { get; set; }
	}
}
