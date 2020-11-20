using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SmartLabeling.API.HealthChecks;
using SmartLabeling.API.Services;
using SmartLabeling.Core.Hubs;
using SmartLabeling.Core.Interfaces;
using SmartLabeling.Core.Models;
using System.Text.Json.Serialization;

namespace SmartLabeling.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.Configure<ApiSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ApiSettings>>().Value);
            services.AddSingleton<ICameraService, FakeCameraService>();
            services.AddSingleton<ISensorsService, FakeSensorsService>();

            services.AddSignalR();

            //TODO add health check for Camera and Sensors Iot devices
            services.AddHealthChecks().AddCheck<FakeHealthCheck>("Fake health check");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartLabeling", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHealthChecks("/api/v1/health");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartLabeling v1"));
            }

            app.UseFileServer();

            //app.UseHttpsRedirection();

            app.UseRouting();

            var cameraHub = Configuration.GetSection("AppSettings").GetValue<string>("CameraHub");
            var sensorsHub = Configuration.GetSection("AppSettings").GetValue<string>("SensorsHub");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<CameraHub>(cameraHub);
                endpoints.MapHub<SensorsHub>(sensorsHub);
                endpoints.MapControllers();
            });
        }
    }
}
