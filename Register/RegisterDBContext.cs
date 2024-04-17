using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.Models.Repository;
using WebAPI.Entities;
using WebAPI.Interface;

namespace WebAPI.Register
{
    public static class RegisterAllDBContext
    {
        public static IServiceCollection RegisterDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PracticeContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AppConfig:ConnectionStrings:DbConnection"));
            });

            services.AddScoped<ICurrencyRepositoty, CurrencyRepository>();

            return services;
        }
    }
}
