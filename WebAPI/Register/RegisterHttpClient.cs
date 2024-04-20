using System.Net.Http.Headers;
using WebAPI.Handler;

namespace WebAPI.Register
{
    public static class RegisterAllHttpClient
    {
        public static IServiceCollection RegisterHttpClient(this IServiceCollection services, IConfiguration configuration)
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
