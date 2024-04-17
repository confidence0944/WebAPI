using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http;
using WebAPI.Model;
using WebAPI.Service;
using WebAPI.Handler;
using WebAPI.Interface;
using Web.Models.Repository;

namespace WebAPI.Register
{
    public static class RegisterAllServices
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSetting>(configuration.GetSection("AppConfig"));

            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IRateService, RateService>();

            services.AddScoped<ICurrencyRepositoty, CurrencyRepository>();
            return services;
        }

        public static IServiceCollection RegisterHttpClientFactory(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<HttpClientLoggingHandler>();
            services.AddHttpClient();

            string address = configuration.GetValue<string>("AppConfig:CoindeskApiUrl");
            services.AddHttpClient("CoindeskHttpClient", c =>
            {
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" });
                c.BaseAddress = new Uri(address);
            }).AddHttpMessageHandler<HttpClientLoggingHandler>();

            return services;
        }
    }
}
