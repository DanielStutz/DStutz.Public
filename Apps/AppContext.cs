using DStutz.Apps.Services;

namespace DStutz.Apps
{
    public interface IAppContext
    {
        public IAppConfig AppConfig { get; }

        public S? GetService<S>();
        public S GetRequiredService<S>() where S : notnull;

        public ILogger CreateLogger(string categoryName);
        public ILogger CreateLogger(object caller);
        public ILogger<T> CreateLogger<T>();

        public ServiceContext GetServiceContext(FileInfo? configFileInfo = null);
        public ServiceContext GetServiceContext(string configUniqueId);
        public ServiceContext GetServiceContext(string configUniqueId, Client client);
        public ServiceRepositoryContext GetServiceRepositoryContext();
    }

    public class AppContext
        : IAppContext
    {
        #region Properties
        /***********************************************************/
        private IHostEnvironment HostEnvironment { get; }
        private IServiceProvider ServiceProvider { get; }
        public IAppLogger AppLogger { get; }
        public IAppConfig AppConfig { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public AppContext(
            IHostEnvironment env,
            IServiceProvider pro,
            IAppLogger log,
            IAppConfig con)
        {
            HostEnvironment = env;
            ServiceProvider = pro;
            AppConfig = con;
            AppLogger = log;

            log.LogStart<AppContext>();
        }
        #endregion

        #region Methods - Context
        /***********************************************************/
        public ServiceContext GetServiceContext(
            FileInfo? configFileInfo)
        {
            return new ServiceContext(this, configFileInfo);
        }

        public ServiceContext GetServiceContext(
            string configUniqueId)
        {
            return new ServiceContext(this, configUniqueId);
        }

        public ServiceContext GetServiceContext(
            string configUniqueId,
            Client client)
        {
            return new ServiceContext(this, configUniqueId, client);
        }

        public ServiceRepositoryContext GetServiceRepositoryContext()
        {
            return new ServiceRepositoryContext(this);
        }
        #endregion

        #region Methods - Logger
        /***********************************************************/
        public ILogger CreateLogger(string categoryName)
        {
            return AppLogger.LoggerFactory.CreateLogger(categoryName);
        }

        public ILogger CreateLogger(object caller)
        {
            return CreateLogger(caller.GetType().Name);
        }

        public ILogger<T> CreateLogger<T>()
        {
            return AppLogger.LoggerFactory.CreateLogger<T>();
        }
        #endregion

        #region Methods - Service
        /***********************************************************/
        public S? GetService<S>()
        {
            return ServiceProvider.GetService<S>();
        }

        public S GetRequiredService<S>()
            where S : notnull
        {
            return ServiceProvider.GetRequiredService<S>();
        }
        #endregion
    }
}
