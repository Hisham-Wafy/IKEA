using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Data.Contexts;
using LinkDev.IKEA.DAL.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Configure Services
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<ApplicationDbContext>(options => 
			{

				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			builder.Services.AddScoped<IDepartmentService, DepartmentService>();
			
			#endregion

			var app = builder.Build();

			#region Configure Kestrel Middle wares

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			// Redirect from HTTP to HTTPS
			app.UseHttpsRedirection();
			// to allow the use files of wwwroot
			app.UseStaticFiles();

			// To read the URL from the browser, it then performs the appropriate action
			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			#endregion

			app.Run();
		}
	}
}
