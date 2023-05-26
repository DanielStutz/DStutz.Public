using DStutz.Apps.Controllers.API;
using DStutz.Apps.Services;
using DStutz.Apps.Services.Base.Configs;
using DStutz.Apps.Services.Base.SQL;
using DStutz.Apps.Utils;
using DStutz.Data;
using DStutz.Data.Cruders;
using DStutz.Data.Pocos.Companies;

namespace DStutz.Apps
{
    public interface IAppLogger
    {
        public ILoggerFactory LoggerFactory { get; }
        public ILogger<T> LogStart<T>(IAppConfig? c = null);
        public ILogger<T> LogStop<T>();
    }

    public class AppLogger
        : IAppLogger
    {
        #region Properties
        /***********************************************************/
        public ILoggerFactory LoggerFactory { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public AppLogger(
            ILoggerFactory fac)
        {
            LoggerFactory = fac;
            _loggerFactory = fac;

            LogStart<AppLogger>();
        }
        #endregion

        #region Methods logging start
        /***********************************************************/
        public ILogger<T> LogStart<T>(
            IAppConfig? c = null)
        {
            ILogger<T> logger = CreateLogger<T>();

            logger.LogInformation(
                "{0} has started",
                typeof(T).Name);

            LogConfig(logger,
                c, true, true);

            return logger;
        }

        public static void LogStart(
            IWorker i)
        {
            i.Logger.LogInformation(
                "Worker {0} has started",
                i.GetType().Name);

            LogConfig(i.Logger,
                i.Context.AppConfig, true, true);
        }

        public static void LogStart(
            IAPIControllerBase i)
        {
            i.Logger.LogInformation(
                "Controller {0} has started",
                i.GetType().Name);

            LogConfig(i.Logger,
                i.Context.AppConfig, true, true);
        }

        public static void LogStart(
            IServiceRepository i)
        {
            i.Logger.LogInformation(
                "Repository {0} has started",
                i.GetType().Name);

            LogConfig(i.Logger,
                i.Context.AppContext.AppConfig, true, true);
        }

        public static void LogStart<C>(
            IService<C> i)
            where C : ServiceConfig
        {
            i.Logger.LogInformation(
                "Service {0} has started",
                i.GetType().Name);

            i.Logger.LogInformation(
                "--> Config:");
            i.Logger.LogInformation(
                "    --> Type: {0}",
                i.Config.GetType().Name);

            if (i.Context.ConfigFileInfo != null)
            {
                i.Logger.LogInformation(
                    "    --> Path: {0}",
                    i.Context.ConfigFileInfo.DirectoryName);
                i.Logger.LogInformation(
                    "    --> File: {0}",
                    i.Context.ConfigFileInfo.Name);
            }

            LogConfig(i.Logger,
                i.Context.AppContext.AppConfig, true, true);

            LogClient(i.Logger,
                i.Context.GetClient(), true);
        }

        public static void LogStart<C>(
            IServiceSQL<C> i)
            where C : ServiceConfigSQL
        {
            LogStart((IService<C>)i);

            i.Logger.LogInformation(
                "--> Database:");

            i.Logger.LogInformation(
                "    --> Type:   {0}",
                i.Config.Type);

            i.Logger.LogInformation(
                "    --> Config: {0}",
                i.Config.Connection);

            if (i.Status == Status.OK)
            {
                i.Logger.LogInformation(
                    "    --> Status: {0}",
                    i.Status);
            }
            else
            {
                i.Logger.LogWarning(
                    "    --> Status: {0}",
                    i.Status);
            }
        }
        #endregion

        #region Methods logging stop
        /***********************************************************/
        public ILogger<T> LogStop<T>()
        {
            ILogger<T> logger = CreateLogger<T>();

            logger.LogInformation(
                "{0} has stopped",
                typeof(T).Name);

            return logger;
        }

        public static void LogStop(
            IServiceRepository r)
        {
            r.Logger.LogInformation(
                "Repository {0} has stopped",
                r.GetType().Name);

            LogConfig(r.Logger,
                r.Context.AppContext.AppConfig, false, false);
        }

        public static void LogStop(
            IService s)
        {
            s.Logger.LogInformation(
                "Service {0} has stopped",
                s.GetType().Name);

            LogConfig(s.Logger,
                s.Context.AppContext.AppConfig, false, false);

            LogClient(s.Logger,
                s.Context.GetClient(), false);

            LogMessages(s.Logger,
                s.Messages);
        }
        #endregion

        #region Methods logging database entities
        /***********************************************************/
        public static void LogEntities(
            IService s,
            params ICruderDAO[] cruders)
        {
            s.Logger.LogInformation(
                "    --> Entities:");

            var width = 0;

            foreach (var cruder in cruders)
                width = global::System.Math.Max(width, cruder.Name.Length);

            foreach (var cruder in cruders)
                s.Logger.LogInformation(
                "        --> {0,-" + width + "}: {1}",
                cruder.Name,
                cruder.Number);
        }

        public static void LogEntities(
            IService s,
            params (string name, long count)[] entities)
        {
            s.Logger.LogInformation(
                "    --> Entities:");

            var width = 0;

            foreach (var entity in entities)
                width = global::System.Math.Max(width, entity.name.Length);

            foreach (var entity in entities)
                s.Logger.LogInformation(
                "        --> {0,-" + width + "}: {1}",
                entity.name,
                entity.count);
        }
        #endregion

        #region Methods logging some information
        /***********************************************************/
        public static void LogAdmin(
            ILogger logger,
            Admin a)
        {
            logger.LogInformation(
                "    --> Admin:");
            logger.LogInformation(
                "        --> Name:  {0}",
                a.GetPreSurName());
            logger.LogInformation(
                "        --> Email: {0}",
                a.Email);
            logger.LogInformation(
                "        --> Phone: {0}",
                a.Phone);
        }

        public static void LogCompany(
            ILogger logger,
            Company c)
        {
            logger.LogInformation(
                "    --> Company:");
            logger.LogInformation(
                "        --> LegalName: {0}",
                c.LegalName);
            logger.LogInformation(
                "        --> TradeName: {0}",
                c.TradeName);
        }

        public static void LogConfig(
            ILogger logger,
            IAppConfig? c,
            bool logAdmin,
            bool logCompany)
        {
            if (c != null)
            {
                logger.LogInformation(
                    "--> App:");
                logger.LogInformation(
                    "    --> Solution: {0}",
                    c.Info.Solution);
                logger.LogInformation(
                    "    --> Project:  {0}",
                    c.Info.Project);
                logger.LogInformation(
                    "    --> UniqueId: {0}",
                    c.Info.UniqueId);
                logger.LogInformation(
                    "    --> Language: {0}",
                    c.Info.CultureInfo);

                if (logCompany)
                    LogCompany(logger, c.Company);

                if (logAdmin)
                    LogAdmin(logger, c.Admin);
            }
        }

        public static void LogClient(
            ILogger logger,
            Client? c,
            bool logCompany)
        {
            if (c != null)
            {
                logger.LogInformation(
                    "--> Client:");
                logger.LogInformation(
                    "    --> UniqueId: {0}",
                    c.Info.UniqueId);
                logger.LogInformation(
                    "    --> Language: {0}",
                    c.Info.CultureInfo);

                if (logCompany)
                    LogCompany(logger, c.Company);
            }
        }

        public static void LogMessages(
            ILogger logger,
            ICollection<ServiceMessage> messages)
        {
            if (messages.Count > 0)
            {
                logger.LogInformation(
                    "--> Messages");

                foreach (var m in messages)
                    logger.Log(
                        m.Level,
                        "    --> " + m.Message,
                        m.Args);
            }
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        private static ILoggerFactory? _loggerFactory;

        public static ILogger CreateLogger(
            string categoryName)
        {
            return _loggerFactory!.CreateLogger(categoryName);
        }

        public static ILogger CreateLogger(
            object caller)
        {
            return CreateLogger(caller.GetType().Name);
        }

        public static ILogger<T> CreateLogger<T>()
        {
            return _loggerFactory!.CreateLogger<T>();
        }
        #endregion
    }
}
