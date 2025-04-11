namespace IMS.Application.DTOs
{
	public class OrderDto
	{
		public int Id { get; set; }
		public decimal TotalAmmount { get; set; }
		public DateTime Date { get; set; }
		public string StripePaymentId { get; set; } = string.Empty;
	}
}
