namespace IMS.Application.Interfaces
{
	public interface IPaymentService
	{
		Task<string> ProcessPaymentAsync(decimal amount, string currency);
	}
}
