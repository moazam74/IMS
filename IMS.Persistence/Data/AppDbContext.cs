using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.Persistence.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<Product> Products => Set<Product>();
		public DbSet<Category> Categories => Set<Category>();
		public DbSet<Supplier> Suppliers => Set<Supplier>();
		public DbSet<Order> Orders => Set<Order>();
		public DbSet<OrderItem> OrderItems => Set<OrderItem>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Relationships
			modelBuilder.Entity<Product>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId);

			modelBuilder.Entity<Product>()
				.HasOne(p => p.Supplier)
				.WithMany(s => s.Products)
				.HasForeignKey(p => p.SupplierId);

			modelBuilder.Entity<OrderItem>()
				.HasOne(oi => oi.Product)
				.WithMany()
				.HasForeignKey(oi => oi.ProductId);

			modelBuilder.Entity<OrderItem>()
				.HasOne(oi => oi.Order)
				.WithMany(o => o.OrderItems)
				.HasForeignKey(oi => oi.OrderId);
		}
	}
}
