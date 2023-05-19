using Serilog;

namespace DStutz.Apps
{
    public abstract class AppBaseCon
        : AppBase
    {
        #region Constructors
        /***********************************************************/
        protected AppBaseCon(string[] args)
            : base(args)
        {
            try
            {
                // Get the HostBuilder
                IHostBuilder hb = Host.CreateDefaultBuilder(args);
                ConfigureHostBuilder(hb);
                ConfigureServiceCollection(hb);

                IHost host = hb.Build();
                host.Run();
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
            IHostBuilder builder)
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
            IHostBuilder builder)
        {
            builder
            .ConfigureServices((context, services) =>
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
            });
        }
        #endregion
    }
}
