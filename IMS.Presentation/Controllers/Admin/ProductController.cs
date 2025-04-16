using IMS.Application.Interfaces;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IMS.Presentation.Controllers.Admin
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;
		private readonly ISupplierService _supplierService;

		public ProductController(IProductService productService, ICategoryService categoryService, ISupplierService supplierService)
		{
			_productService = productService;
			_categoryService = categoryService;
			_supplierService = supplierService;
		}

		public async Task<IActionResult> Index()
		{
			var products = await _productService.GetAllAsync();
			return View(products);
		}

		public async Task<IActionResult> Create()
		{
			ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
			ViewBag.Suppliers = new SelectList(await _supplierService.GetAllAsync(), "Id", "Name");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Product product)
		{
			if (ModelState.IsValid)
			{
				await _productService.AddAsync(product);
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", product.CategoryId);
			ViewBag.Suppliers = new SelectList(await _supplierService.GetAllAsync(), "Id", "Name", product.SupplierId);
			return View(product);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var product = await _productService.GetByIdAsync(id);
			if (product == null) return NotFound();

			ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", product.CategoryId);
			ViewBag.Suppliers = new SelectList(await _supplierService.GetAllAsync(), "Id", "Name", product.SupplierId);
			return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				await _productService.UpdateAsync(product);
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", product.CategoryId);
			ViewBag.Suppliers = new SelectList(await _supplierService.GetAllAsync(), "Id", "Name", product.SupplierId);
			return View(product);
		}

		public async Task<IActionResult> Details(int id)
		{
			var product = await _productService.GetByIdAsync(id);
			if (product == null) return NotFound();
			return View(product);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var product = await _productService.GetByIdAsync(id);
			if (product == null) return NotFound();
			return View(product);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _productService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
