using System.Diagnostics;

namespace DStutz.Apps
{
    public abstract class AppBaseConWorker
        : IWorker, IHostedService
    {
        #region Properties
        /***********************************************************/
        public IAppContext Context { get; }
        public ILogger Logger { get; }
        private IHostApplicationLifetime Lifetime { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected AppBaseConWorker(
            IAppContext appContext,
            IHostApplicationLifetime lifetime)
        {
            Context = appContext;
            Logger = Context.CreateLogger(this);
            Lifetime = lifetime;

            AppLogger.LogStart(this);
        }
        #endregion

        #region Methods working
        /***********************************************************/
        protected abstract void Work();

        public Task StartAsync(CancellationToken token)
        {
            Logger.LogDebug(
                "Worker.StartAsync()");

            Lifetime.ApplicationStarted.Register(() =>
            {
                Logger.LogDebug(
                    "Worker.AppLifetime.ApplicationStarted");

                Task.Run(() =>
                {
                    try
                    {
                        Logger.LogDebug(
                            "Worker.Task.Run()");

                        var stopwatch = new Stopwatch();
                        stopwatch.Start();

                        Work();

                        stopwatch.Stop();

                        Logger.LogInformation(
                            stopwatch.ElapsedMilliseconds + " ms");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(
                            ex, "Unable to Worker.Task.Run()");
                        Console.WriteLine(ex);
                        throw;
                    }
                    finally
                    {
                        Lifetime.StopApplication();
                    }
                });
            });

            Lifetime.ApplicationStopping.Register(() =>
            {
                Logger.LogDebug(
                    "Worker.AppLifetime.ApplicationStopping");
            });

            Lifetime.ApplicationStopped.Register(() =>
            {
                Logger.LogDebug(
                    "Worker.AppLifetime.ApplicationStopped");
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken token)
        {
            Logger.LogDebug(
                "Worker.StopAsync()");

            return Task.CompletedTask;
        }
        #endregion
    }
}
