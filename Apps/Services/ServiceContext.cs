using DStutz.Apps.Services.Base.Configs;

namespace DStutz.Apps.Services
{
    public class ServiceContext
    {
        #region Properties
        /***********************************************************/
        public IAppContext AppContext { get; }
        public Client? Client { get; }
        public string? ConfigUniqueId { get; }
        public FileInfo? ConfigFileInfo { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public ServiceContext(
            IAppContext appContext,
            FileInfo? configFileInfo = null)
        {
            AppContext = appContext;
            ConfigFileInfo = configFileInfo;
        }

        public ServiceContext(
            IAppContext appContext,
            string configUniqueId)
            : this(
                  appContext,
                  appContext.AppConfig.ServiceConfigs.ConfigFile)
        {
            ConfigUniqueId = configUniqueId;
        }

        public ServiceContext(
            IAppContext appContext,
            string configUniqueId,
            Client configClient)
            : this(
                  appContext,
                  configClient.ServiceConfigs.ConfigFile)
        {
            ConfigUniqueId = configUniqueId;
            Client = configClient;
        }
        #endregion

        #region Methods - Client and config
        /***********************************************************/
        public Client? GetClient()
        {
            return Client;
        }

        public C GetServiceConfig<C>()
            where C : ServiceConfig
        {
            if (ConfigUniqueId == null)
                throw new NullReferenceException();

            if (Client != null)
                return Client.ServiceConfigs.GetConfig<C>(ConfigUniqueId);

            return AppContext.AppConfig.ServiceConfigs.GetConfig<C>(ConfigUniqueId);
        }
        #endregion

        #region Methods - Logger
        /***********************************************************/
        public ILogger CreateLogger(string categoryName)
        {
            return AppContext.CreateLogger(categoryName);
        }

        public ILogger CreateLogger(object caller)
        {
            return AppContext.CreateLogger(caller);
        }

        public ILogger<T> CreateLogger<T>()
        {
            return AppContext.CreateLogger<T>();
        }
        #endregion

        #region Methods - Service
        /***********************************************************/
        public S? GetService<S>()
        {
            return AppContext.GetService<S>();
        }

        public S GetRequiredService<S>()
            where S : notnull
        {
            return AppContext.GetRequiredService<S>();
        }
        #endregion
    }
}
