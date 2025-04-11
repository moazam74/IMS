using IMS.Application.Interfaces;
using Stripe;

namespace IMS.Infrastructure.Payment
{
	public class PaymentService : IPaymentService
	{
		public PaymentService()
		{
			StripeConfiguration.ApiKey = "sk_test_XXXXXXXXXXXXXXXX"; // Replace in production
		}

		public async Task<string> ProcessPaymentAsync(decimal amount, string currency)
		{
			var options = new PaymentIntentCreateOptions
			{
				Amount = (long)(amount * 100), // Stripe uses smallest currency unit
				Currency = currency,
				PaymentMethodTypes = new List<string> { "card" }
			};

			var service = new PaymentIntentService();
			var intent = await service.CreateAsync(options);
			return intent.Id;
		}
	}
}
