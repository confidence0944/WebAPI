using NLog;
using NLog.Web;
using WebAPI.Handler;
using WebAPI.Register;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            builder.Services.AddControllers().AddNewtonsoftJson().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.PropertyNamingPolicy = null;
                option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //���UApiLog
            builder.ApplyNLog();

            //���UService
            builder.Services.RegisterServices(builder.Configuration);

            //���UDBContext
            builder.Services.RegisterDBContext(builder.Configuration);

            //���UHttpClient�A�Ȥ�Log
            builder.Services.RegisterHttpClientFactory(builder.Configuration);

            var app = builder.Build();

            app.UseMiddleware<ApiLoggingMiddleware>();
            app.ConfigureExceptionHandler(logger);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}