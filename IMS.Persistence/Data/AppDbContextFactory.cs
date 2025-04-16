using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IMS.Persistence.Data
{
	public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		public AppDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

			var connectionString = "Server=localhost;Database=IMSDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

			// Configure to use SQL Server with the connection string
			optionsBuilder.UseSqlServer(connectionString);

			return new AppDbContext(optionsBuilder.Options);
		}
	}
}
