using Serilog;

namespace DStutz.Apps
{
    public abstract class AppBaseWeb
        : AppBase
    {
        #region Constructors
        /***********************************************************/
        protected AppBaseWeb(string[] args)
            : base(args)
        {
            try
            {
                // Get the WebApplicationBuilder
                WebApplicationBuilder wab = WebApplication.CreateBuilder(args);
                ConfigureHostBuilder(wab.Host);
                ConfigureServiceCollection(wab.Services);

                WebApplication wa = wab.Build();
                ConfigureWebApplication(wa);
                wa.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        #endregion

        #region Methods configuring
        /***********************************************************/
        protected virtual void ConfigureHostBuilder(
            ConfigureHostBuilder builder)
        {
            builder
                .ConfigureHostConfiguration(builder =>
                {
                    base.ConfigureHostConfiguration(builder);
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    base.ConfigureAppConfiguration(context, builder);
                })
                .ConfigureLogging((context, builder) =>
                {
                    base.ConfigureLogging(context, builder);
                })
                .UseSerilog();
        }

        protected virtual void ConfigureServiceCollection(
            IServiceCollection services)
        {
            LogDebug(
                "--> ConfigureServices()");

            services
                .AddSingleton<
                    IAppLogger,
                    AppLogger>()
                .AddSingleton<
                    IAppConfig,
                    AppConfig>()
                .AddSingleton<
                    IAppContext,
                    AppContext>();
        }

        protected virtual void ConfigureWebApplication(
            WebApplication application)
        {
            //
        }
        #endregion
    }
}
