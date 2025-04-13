using IMS.Application.DTOs;
using IMS.Application.Interfaces;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Text.Json;

namespace IMS.Presentation.Controllers.User
{
	public class CheckoutController : Controller
	{
		private readonly IPaymentService _paymentService;
		private readonly IOrderService _orderService;
		private readonly IProductService _productService;

		public CheckoutController(IPaymentService paymentService, IOrderService orderService, IProductService productService)
		{
			_paymentService = paymentService;
			_orderService = orderService;
			_productService = productService;
		}

		public IActionResult Index()
		{
			var cart = GetCart();
			if (!cart.Any()) return RedirectToAction("Index", "UserProduct");
			return View(cart);
		}

		[HttpPost]
		public async Task<IActionResult> Process()
		{
			var cart = GetCart();
			if (!cart.Any()) return RedirectToAction("Index", "UserProduct");

			// Create Stripe Session
			var options = new SessionCreateOptions
			{
				PaymentMethodTypes = new List<string> { "card" },
				LineItems = cart.Select(item => new SessionLineItemOptions
				{
					PriceData = new SessionLineItemPriceDataOptions
					{
						Currency = "usd",
						UnitAmount = (long)(item.UnitPrice * 100),
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = item.ProductName
						}
					},
					Quantity = item.Quantity
				}).ToList(),
				Mode = "payment",
				SuccessUrl = $"{Request.Scheme}://{Request.Host}/Checkout/Success",
				CancelUrl = $"{Request.Scheme}://{Request.Host}/Checkout/Index"
			};

			var service = new SessionService();
			Session session = await service.CreateAsync(options);
			HttpContext.Session.SetString("stripeSessionId", session.Id);
			return Redirect(session.Url);
		}

		public async Task<IActionResult> Success()
		{
			var cart = GetCart();
			if (!cart.Any()) return RedirectToAction("Index", "UserProduct");

			var sessionId = HttpContext.Session.GetString("stripeSessionId");
			if (string.IsNullOrEmpty(sessionId)) return RedirectToAction("Index");

			// Save order
			var order = new Order
			{
				Date = DateTime.Now,
				StripePaymentId = sessionId,
				TotalAmount = cart.Sum(c => c.Total),
				OrderItems = cart.Select(c => new OrderItem
				{
					ProductId = c.ProductId,
					Quantity = c.Quantity,
					Price = c.UnitPrice
				}).ToList()
			};

			await _orderService.CreateOrderAsync(order);

			// Clear cart
			HttpContext.Session.Remove("cart");
			return View();
		}

		private List<CartItemDto> GetCart()
		{
			var json = HttpContext.Session.GetString("cart");
			return string.IsNullOrEmpty(json) ? new() : JsonSerializer.Deserialize<List<CartItemDto>>(json)!;
		}
	}
}
