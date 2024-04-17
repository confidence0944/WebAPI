using NLog.Web;
using NLog;

namespace WebAPI.Register
{
    public static class RegisterNLog
    {
        /// <summary>
        /// 讓應用程式使用NLog
        /// </summary>
        /// <param name="builder"></param>
        public static void ApplyNLog(this WebApplicationBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            var config = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("nlog.json")
                           .Build();

            LogManager.Configuration = new NLog.Extensions.Logging.NLogLoggingConfiguration(config.GetSection("NLog"));
            builder.Logging.AddNLogWeb(LogManager.Configuration);
        }
    }
}
