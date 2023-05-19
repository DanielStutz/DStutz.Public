using DStutz.Apps.Services.Base.Configs;
using DStutz.System.IO;

namespace DStutz.Apps.Services.Base
{
    public interface IServiceList<T>
        : IService
        where T : IJoinable
    {
        public IList<T> List { get; }
    }

    public class ServiceList<T>
        : ServiceList<ServiceConfigMock, T>
        where T : IJoinable
    {
        #region Constructors
        /***********************************************************/
        public ServiceList(
            IAppContext appContext)
            : base(
                  appContext,
                  new ServiceConfigMock())
        { }

        protected ServiceList(
            ServiceContext context,
            bool loadDataFromFile = false)
            : base(
                  context,
                  new ServiceConfigMock(),
                  loadDataFromFile)
        { }

        protected ServiceList(
            ServiceContext context,
            params Newtonsoft.Json.JsonConverter[] converters)
            : base(
                  context,
                  new ServiceConfigMock(),
                  converters)
        { }
        #endregion
    }

    public class ServiceList<C, T>
        : Service<C>, IServiceList<T>
        where C : ServiceConfig
        where T : IJoinable
    {
        #region Properties
        /***********************************************************/
        public IList<T> List { get; } = new List<T>();
        #endregion

        #region Constructors
        /***********************************************************/
        public ServiceList(
            IAppContext appContext,
            C config)
            : base(
                  appContext.GetServiceContext(),
                  config)
        {
            AppLogger.LogStart(this);
        }

        protected ServiceList(
            ServiceContext context,
            C config,
            bool loadDataFromFile = false)
            : base(
                  context,
                  config)
        {
            // Load from file or start with an empty list
            if (loadDataFromFile)
                List = FileReaderJson.ReadList<T>(
                    Context.ConfigFileInfo);

            AppLogger.LogStart(this);
        }

        protected ServiceList(
            ServiceContext context,
            C config,
            params Newtonsoft.Json.JsonConverter[] converters)
            : base(
                  context,
                  config)
        {
            List = FileReaderJson.ReadList<T>(
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
                "--> Loading {0} to a list",
                typeof(T).Name);

            foreach (var item in List)
                Logger.LogInformation(
                    "    --> {0}",
                    item.Joiner.Row);

            Logger.LogInformation(
                "--> Loaded {0} instance(s) of {1}",
                List.Count,
                typeof(T).Name);
        }
        #endregion
    }
}
