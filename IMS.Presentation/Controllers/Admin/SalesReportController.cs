using IMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Presentation.Controllers.Admin
{
	public class SalesReportController : Controller
	{
		private readonly IReportService _reportService;

		public SalesReportController(IReportService reportService)
		{
			_reportService = reportService;
		}

		public IActionResult Index()
		{
			// show date filter form
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(DateTime startDate, DateTime endDate)
		{
			var report = await _reportService.GetSalesByCategoryAsync(startDate, endDate);
			ViewBag.StartDate = startDate.ToShortDateString();
			ViewBag.EndDate = endDate.ToShortDateString();
			return View(report);
		}
	}
}
