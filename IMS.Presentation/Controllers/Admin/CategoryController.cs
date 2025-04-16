using IMS.Application.Interfaces;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Presentation.Controllers.Admin
{
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public async Task<IActionResult> Index()
		{
			var categories = await _categoryService.GetAllAsync();
			return View(categories);
		}

		public IActionResult Create() => View();

		[HttpPost]
		public async Task<IActionResult> Create(Category category)
		{
			if (ModelState.IsValid)
			{
				await _categoryService.AddAsync(category);
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var category = await _categoryService.GetByIdAsync(id);
			if (category == null) return NotFound();
			return View(category);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Category category)
		{
			if (ModelState.IsValid)
			{
				await _categoryService.UpdateAsync(category);
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var category = await _categoryService.GetByIdAsync(id);
			if (category == null) return NotFound();
			return View(category);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _categoryService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
