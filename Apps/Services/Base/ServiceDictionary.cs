using DStutz.Apps.Services.Base.Configs;
using DStutz.System.IO;

using Newtonsoft.Json;

namespace DStutz.Apps.Services.Base
{
    public interface IServiceDictionary<K, V>
        : IService
        where K : notnull
        where V : IJoinable
    {
        public IDictionary<K, V> Dictionary { get; }
    }

    public class ServiceDictionary<K, V>
        : ServiceDictionary<ServiceConfigMock, K, V>
        where K : notnull
        where V : IJoinable
    {
        #region Constructors
        /***********************************************************/
        public ServiceDictionary(
            IAppContext appContext)
            : base(
                  appContext,
                  new ServiceConfigMock())
        { }

        protected ServiceDictionary(
            ServiceContext context,
            bool loadDataFromFile = false)
            : base(
                  context,
                  new ServiceConfigMock(),
                  loadDataFromFile)
        { }

        protected ServiceDictionary(
            ServiceContext context,
            params JsonConverter[] converters)
            : base(
                  context,
                  new ServiceConfigMock(),
                  converters)
        { }
        #endregion
    }

    public class ServiceDictionary<C, K, V>
        : Service<C>, IServiceDictionary<K, V>
        where C : ServiceConfig
        where K : notnull
        where V : IJoinable
    {
        #region Properties
        /***********************************************************/
        public IDictionary<K, V> Dictionary { get; } = new Dictionary<K, V>();
        #endregion

        #region Constructors
        /***********************************************************/
        public ServiceDictionary(
            IAppContext appContext,
            C config)
            : base(
                  appContext.GetServiceContext(),
                  config)
        {
            AppLogger.LogStart(this);
        }

        protected ServiceDictionary(
            ServiceContext context,
            C config,
            bool loadDataFromFile = false)
            : base(
                  context,
                  config)
        {
            // Load from file or start with an empty dictionary
            if (loadDataFromFile)
                Dictionary = FileReaderJson.ReadDictionary<K, V>(
                    Context.ConfigFileInfo);

            AppLogger.LogStart(this);
        }

        protected ServiceDictionary(
            ServiceContext context,
            C config,
            params JsonConverter[] converters)
            : base(
                  context,
                  config)
        {
            Dictionary = FileReaderJson.ReadDictionary<K, V>(
                Context.ConfigFileInfo,
                null,
                converters);

            AppLogger.LogStart(this);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        protected void LogData()
        {
            Logger.LogInformation(
                "--> Loading {0} to a dictionary",
                typeof(V).Name);

            var max = Dictionary.Keys.Select(e => e.ToString()!.Length).Max();

            foreach (var kvp in Dictionary)
                Logger.LogInformation(
                    "    --> key {0,-" + max + "} maps to value {1}",
                    kvp.Key,
                    kvp.Value.Joiner.Row);

            Logger.LogInformation(
                "--> Loaded {0} instance(s) of {1}",
                Dictionary.Count,
                typeof(V).Name);
        }
        #endregion
    }
}
