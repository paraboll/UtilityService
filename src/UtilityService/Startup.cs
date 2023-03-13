using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System.Reflection;
using UtilityService.Repository;
using UtilityService.Repository.Interfaces;

namespace UtilityService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Включаем конфиг.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configuration = builder.Build();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog("NLog.config");
            });

            services.AddControllersWithViews();

            services.AddDbContext<UtilityDBContext>(options =>
            {
                var dbPath = Configuration["DBConfig:Path"];
                options.UseSqlite($"Data Source={dbPath};", options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            });

            services.AddScoped<IHistoryCalculationsRepository, HistoryCalculationsRepository>();
            services.AddScoped<IHistoryCounterValuesRepository, HistoryCounterValuesRepository>();
            services.AddScoped<ICurrentCoefficientRepository, CurrentCoefficientRepository>();
            services.AddScoped<IHistoryCoefficientRepository, HistoryCoefficientRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
