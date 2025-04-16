using IMS.Application.DTOs;
using IMS.Application.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace IMS.Infrastructure.Repositories
{
	public class ReportService : IReportService
	{
		private readonly IConfiguration _configuration;

		public ReportService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<List<SalesReportDto>> GetSalesByCategoryAsync(DateTime startDate, DateTime endDate)
		{
			var result = new List<SalesReportDto>();
			var connStr = _configuration.GetConnectionString("DefaultConnection");

			using var conn = new SqlConnection(connStr);
			using var cmd = new SqlCommand("GetSalesByCategory", conn)
			{
				CommandType = CommandType.StoredProcedure
			};

			cmd.Parameters.AddWithValue("@StartDate", startDate);
			cmd.Parameters.AddWithValue("@EndDate", endDate);

			await conn.OpenAsync();
			using var reader = await cmd.ExecuteReaderAsync();

			while (await reader.ReadAsync())
			{
				result.Add(new SalesReportDto
				{
					CategoryName = reader["CategoryName"].ToString(),
					TotalSales = Convert.ToDecimal(reader["TotalSales"])
				});
			}

			return result;
		}
	}
}
