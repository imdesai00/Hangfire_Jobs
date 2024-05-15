using Hangfire;
using Hangfire.Client;
using Hangfire.PostgreSql;
using Microsoft.OpenApi.Models;
using Hangfire.DataAccess;
using Hangfire.InterFace;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Hangfire
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static string HangFireConnectionString { get; private set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<HangfireBackgroundService>();
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(Configuration.GetConnectionString("HangfireConnection")));
            services.AddHangfireServer();
            services.AddSingleton<IJobTest, JobTest>();
            services.AddControllers();
            services.AddSwaggerGen();
            HangFireConnectionString = Configuration["ConnectionStrings:HangfireConnection"];
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IJobTest jobTest)
        {
            app.UseCors("AllowSpecificOrigin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1"));
            }
            else
            {
                app.UseExceptionHandler();
            }
            app.UseHangfireDashboard();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
