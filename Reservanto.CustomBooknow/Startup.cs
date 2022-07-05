using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservanto.CustomBooknow.Code;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;

namespace Reservanto.CustomBooknow
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddRazorPages().AddRazorRuntimeCompilation();

			// Přidání API klienta Reservanta do registrovaných služeb.
			services.AddSingleton<ReservantoApiConnector>();

			// Přidání i provázání s konfugurací API.
			services.Configure<ReservantoApiConfiguration>(this.Configuration.GetSection("Api"));

			services.AddMvc(config =>
			{
				config.Filters.Add(typeof(LongTimeTokenExceptionFilter));
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();
	
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Modal}/{action=Index}/{id?}");
			});
		}
	}
}
