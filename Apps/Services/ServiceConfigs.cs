using DStutz.Apps.Services.Base.Configs;
using DStutz.System.Exceptions;

namespace DStutz.Apps.Services
{
    public interface IServiceConfigs
    {
        public FileInfo ConfigFile { get; }
        public T GetConfig<T>(string uniqueId) where T : ServiceConfig;
    }

    public class ServiceConfigs
        : IServiceConfigs
    {
        #region Properties
        /***********************************************************/
        public ICollection<ServiceConfigAPI>? API { get; set; }
        public ICollection<ServiceConfigFiles>? File { get; set; }
        public ICollection<ServiceConfigEmails>? Mail { get; set; }
        public ICollection<ServiceConfigSQLMySql>? MySql { get; set; }
        public ICollection<ServiceConfigSQLSqlite>? Sqlite { get; set; }
        #endregion

        #region Properties additional
        /***********************************************************/
        public FileInfo ConfigFile { get; set; }
        private List<ServiceConfig> Configs { get; } = new();
        #endregion

        #region Methods
        /***********************************************************/
        public void Init(
            FileInfo configFile,
            ILogger? logger = null)
        {
            ConfigFile = configFile;

            if (API != null)
                Configs.AddRange(API);

            if (File != null)
                Configs.AddRange(File);

            if (Mail != null)
                Configs.AddRange(Mail);

            if (MySql != null)
                Configs.AddRange(MySql);

            if (Sqlite != null)
                Configs.AddRange(Sqlite);

            if (logger != null)
            {
                foreach (var config in Configs)
                    logger.LogInformation(
                        "    --> {0}",
                        config.Joiner.Row);

                logger.LogInformation(
                    "    --> Loaded {0} service configs from file {1}",
                    Configs.Count,
                    ConfigFile.FullName);
            }
        }

        public T GetConfig<T>(
            string uniqueIdConfig)
            where T : ServiceConfig
        {
            if (Configs.Count == 0)
            {
            }

            foreach (var config in Configs)
                if (config.GetType() == typeof(T) &&
                    config.UniqueId.Equals(uniqueIdConfig))
                    return (T)config;

            throw new NotFoundException(nameof(T), uniqueIdConfig);
        }
        #endregion
    }
}
