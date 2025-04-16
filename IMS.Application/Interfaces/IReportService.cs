using IMS.Application.DTOs;

namespace IMS.Application.Interfaces
{
	public interface IReportService
	{
		Task<List<SalesReportDto>> GetSalesByCategoryAsync(DateTime startDate, DateTime endDate);
	}
}
