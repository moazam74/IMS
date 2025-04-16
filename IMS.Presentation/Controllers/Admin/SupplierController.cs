using IMS.Application.Interfaces;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Presentation.Controllers.Admin
{
	public class SupplierController : Controller
	{
		private readonly ISupplierService _supplierService;

		public SupplierController(ISupplierService supplierService)
		{
			_supplierService = supplierService;
		}

		public async Task<IActionResult> Index()
		{
			var suppliers = await _supplierService.GetAllAsync();
			return View(suppliers);
		}

		public IActionResult Create() => View();

		[HttpPost]
		public async Task<IActionResult> Create(Supplier supplier)
		{
			if (ModelState.IsValid)
			{
				await _supplierService.AddAsync(supplier);
				return RedirectToAction(nameof(Index));
			}
			return View(supplier);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var supplier = await _supplierService.GetByIdAsync(id);
			if (supplier == null) return NotFound();
			return View(supplier);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Supplier supplier)
		{
			if (ModelState.IsValid)
			{
				await _supplierService.UpdateAsync(supplier);
				return RedirectToAction(nameof(Index));
			}
			return View(supplier);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var supplier = await _supplierService.GetByIdAsync(id);
			if (supplier == null) return NotFound();
			return View(supplier);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _supplierService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
