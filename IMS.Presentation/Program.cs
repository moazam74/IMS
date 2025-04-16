using IMS.Application.Interfaces;
using IMS.Infrastructure.Payment;
using IMS.Infrastructure.Repositories;
using IMS.Persistence.Data;
using IMS.Presentation.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();


// 🔹 Configure EF Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Register Application Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

Stripe.StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];


var app = builder.Build();

// 🔹 Configure HTTP pipeline
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// 🔹 Add global exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 🔹 Configure MVC routes
app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=UserProduct}/{action=Index}/{id?}");

app.Run();
