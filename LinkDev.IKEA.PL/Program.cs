using LinkDev.IKEA.DAL.Data;
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
