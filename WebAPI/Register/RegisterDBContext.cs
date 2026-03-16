using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.Models.Repository;
using WebAPI.Entities;
using WebAPI.Interface;
using WebAPI.Model;

namespace WebAPI.Register
{
    public static class RegisterAllDBContext
    {      
        public static IServiceCollection RegisterDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var appSetting = new AppSetting();
            configuration.GetSection("AppConfig").Bind(appSetting);

            services.AddDbContext<PracticeContext>(options =>
            {
                if (appSetting.IsUseSqlLite)
                {
                    options.UseSqlite(appSetting.ConnectionStrings.SqlLiteDbConnection);
                }
                else
                {
                    options.UseSqlServer(appSetting.ConnectionStrings.DbConnection);
                }
            }, ServiceLifetime.Scoped); 

            services.AddScoped<ICurrencyRepositoty, CurrencyRepository>();

            return services;
        }
    }
}
