using IMS.Application.DTOs;
using IMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IMS.Presentation.Controllers.User
{
	public class UserProductController : Controller
	{
		private readonly IProductService _productService;

		public UserProductController(IProductService productService)
		{
			_productService = productService;
		}

		public async Task<IActionResult> Index()
		{
			var products = await _productService.GetAllAsync();
			return View(products);
		}

		public IActionResult AddToCart(int id, string name, decimal price)
		{
			var cart = GetCart();
			var existing = cart.FirstOrDefault(c => c.ProductId == id);

			if (existing != null)
				existing.Quantity++;
			else
				cart.Add(new CartItemDto { ProductId = id, ProductName = name, UnitPrice = price, Quantity = 1 });

			SaveCart(cart);
			return RedirectToAction("Cart");
		}

		public IActionResult Cart()
		{
			var cart = GetCart();
			return View(cart);
		}

		public IActionResult RemoveFromCart(int id)
		{
			var cart = GetCart();
			var item = cart.FirstOrDefault(c => c.ProductId == id);
			if (item != null) cart.Remove(item);
			SaveCart(cart);
			return RedirectToAction("Cart");
		}

		private List<CartItemDto> GetCart()
		{
			var json = HttpContext.Session.GetString("cart");
			return string.IsNullOrEmpty(json)
				? new List<CartItemDto>()
				: JsonSerializer.Deserialize<List<CartItemDto>>(json)!;
		}

		private void SaveCart(List<CartItemDto> cart)
		{
			HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));
		}
	}
}
