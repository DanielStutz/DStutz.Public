using DStutz.Apps.Services;
using DStutz.Apps.Utils;
using DStutz.Data.Pocos.Companies;
using DStutz.Data.Pocos.People;
using DStutz.System.Enums;
using DStutz.System.Exceptions;
using DStutz.System.IO;
using DStutz.System.IO.Loaders;
using DStutz.System.Replacers;
using DStutz.System.Serializers;

using System.Globalization;

namespace DStutz.Apps
{
    public interface IAppIO
    {
        public DirectoryInfo GetCodeDir(string path = "");
        public DirectoryInfo GetConfDir(string path = "");
        public DirectoryInfo GetDataDir(bool checkExistence, string path = "");
        public FileInfo GetConfFile(string path);
        public FileInfo GetDataFile(string path);
    }

    public interface IAppConfig
        : IAppIO
    {
        public Admin Admin { get; }
        public Info Info { get; }
        public Company Company { get; }
        public ServiceConfigs ServiceConfigs { get; }
        public Client GetClient(string uniqueId);
    }

    public class AppConfig
        : IAppConfig
    {
        #region Properties
        /***********************************************************/
        public Admin Admin { get; } = new();
        public Info Info { get; } = new();
        public Company Company { get; } = new();
        public ServiceConfigs ServiceConfigs { get; } = new();
        private IConfiguration Configuration { get; }
        private SortedDictionary<string, Client> Clients { get; } = new();
        #endregion

        #region Constructors
        /***********************************************************/
        public AppConfig(
            //IHostEnvironment env,
            IConfiguration con,
            IAppLogger log)
        {
            Configuration = con;

            var logger = log.LogStart<AppConfig>();

            // Loading sections from file appsettings.json
            con.GetSection("Info").Bind(Info);
            con.GetSection("Admin").Bind(Admin);
            con.GetSection("Company").Bind(Company);
            con.GetSection("Services").Bind(ServiceConfigs);

            logger.LogInformation(
                "--> Loading service configs");

            ServiceConfigs.Init(GetConfFile("appsettings.json"), logger);

            CultureInfo.DefaultThreadCurrentCulture =
                new CultureInfo(Info.CultureInfo);

            // Loading clients
            var dir = GetConfDir();

            var serializer =
                new SerializerFileJSON<Client>(
                    new LoaderFileOptions("json", dir.FullName)
                    {
                        SearchPattern = "Client_*.json",
                        Replacer = new Replacer("Client_", ""),
                    });

            serializer.SetOptions(false, false, true);
            serializer.SetConverters(true, new EnumAbbrJsonConverter<Gender>());

            logger.LogInformation(
                "--> Serializing {0} objects",
                typeof(Client).Name);

            foreach (var file in serializer.Files)
            {
                var uid = file.Key;
                var client =
                    serializer.Read(
                        file.Value,
                        serializer.FileEncoding);

                Clients.Add(uid, client);

                logger.LogInformation(
                    "    --> {0} {1} {2}",
                    uid,
                    file.Value.FullName,
                    client.Info.Date);

                client.ServiceConfigs.Init(file.Value, logger);
            }

            logger.LogInformation(
                "    --> Serialized {0} objects from folder {1}",
                Clients.Count,
                dir);
        }
        #endregion

        #region Methods implementing IAppIO
        /***********************************************************/
        public DirectoryInfo GetCodeDir(
            string path = "")
        {
            return Finder.FindDirectory(
                true,
                AppBase.CodePath.FullName,
                path);
        }

        public DirectoryInfo GetConfDir(
            string path = "")
        {
            return Finder.FindDirectory(
                true,
                AppBase.ConfPath.FullName,
                path);
        }

        public DirectoryInfo GetDataDir(
            bool checkExistence,
            string path = "")
        {
            return Finder.FindDirectory(
                checkExistence,
                AppBase.DataPath.FullName,
                path);
        }

        public FileInfo GetConfFile(
            string path)
        {
            return Finder.FindFile(
                true,
                AppBase.ConfPath.FullName,
                path);
        }

        public FileInfo GetDataFile(
            string path)
        {
            return Finder.FindFile(
                true,
                AppBase.DataPath.FullName,
                path);
        }
        #endregion

        #region Methods implementing IAppConfig
        /***********************************************************/
        public Client GetClient(string uniqueId)
        {
            if (Clients.ContainsKey(uniqueId))
                return Clients[uniqueId];

            throw new NotFoundException(
                typeof(Client), uniqueId);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public void Bind(
            object instance)
        {
            Configuration.Bind(instance);
        }

        public void Bind(
            object instance,
            string keySection)
        {
            Configuration.GetSection(keySection).Bind(instance);
        }
        #endregion
    }
}
