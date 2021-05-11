using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ExchangeRatesWorker.Middlewares;
using ExchangeRatesWorker.Logic.Helpers;
using ExchangeRatesWorker.Logic.RemoteServices.ExchangeratesApi;
using ExchangeRatesWorker.Logic.RemoteServices.Api.Exchangerate;
using Swashbuckle.AspNetCore.Swagger;
using ExchangeRatesWorker.Settings;
using Microsoft.OpenApi.Models;

namespace ExchangeRatesWorker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ICurrencySymbolsExRatesApiHelper, CurrencySymbolsExRatesApiHelper>();
            services.AddHttpClient<IExchangeRatesService, ExchangeRatesService>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("ExchangeAPIs:ExchangeratesApi:BaseUrl"));
            });

            services.AddHttpClient<IApiExchangeRateService, ApiExchangeRateService>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("ExchangeAPIs:Api.Exchangerate:BaseUrl"));
                //client.DefaultRequestHeaders.Add("Accept","application/json;");
            });

            var swaggerSettings = new SwaggerSettings();
            Configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = swaggerSettings.Title,
                    Description = swaggerSettings.Description,
                    //Contact = new OpenApiContact
                    //{
                    //    Name = swaggerSettings.ContactName,
                    //    Email = string.Empty,
                    //    Url = new Uri(swaggerSettings.ContactUrl)
                    //}
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<CustomExceptionMiddleware>();

            var swaggerSettings = new SwaggerSettings();
            Configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);
            app.UseSwagger(option => option.RouteTemplate = swaggerSettings.JsonRoute);
            app.UseSwaggerUI(opstion => opstion.SwaggerEndpoint(swaggerSettings.UiEndpoint, swaggerSettings.Description));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
