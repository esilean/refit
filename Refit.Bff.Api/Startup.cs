using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Refit.Bff.Api.Interfaces;
using Refit.Bff.Api.Middlewares;

namespace Refit.Bff.Api
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Refit.Bff.Api", Version = "v1" });
            });

            services
                .AddRefitClient<IPokemonApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://pokeapi.co/api/v2"));

            services.AddResponseCompression();
            services.AddResponseCaching(x =>
            {
                x.MaximumBodySize = 2000000;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Refit.Bff.Api v1"));
            }

            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseMiddleware<LoggingRequestResponseMiddleware>();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
