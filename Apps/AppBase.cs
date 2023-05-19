using DStutz.Apps.Utils;
using DStutz.System.IO;

using Serilog;
using Serilog.Events;

namespace DStutz.Apps
{
    public abstract class AppBase
    {
        #region Properties
        /***********************************************************/
        public string ApplicationName { get; private set; } = "";
        public string EnvironmentName { get; private set; } = "";
        public static DirectoryInfo RootPath { get; private set; }
        public static DirectoryInfo ConfPath { get; private set; }
        public static DirectoryInfo DataPath { get; private set; }
        public static DirectoryInfo CodePath { get; private set; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected AppBase(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override(
                    "Microsoft",
                    LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            Log.Information(
                "{0}:Constructor()", GetType().Name);
            Log.Information(
                "--> Arguments: {0}", args == null ? 0 : args.Length);

            // There are problems with the buffer size of the console?!
            //try
            //{
            //    var h = Console.BufferHeight;
            //    var w = Console.BufferWidth;

            //    Console.BufferHeight = 100;
            //    Console.BufferWidth = 500;

            //    Log.Information(
            //        "--> BufferHeight: {0,3} --> {1,3}",
            //        h,
            //        Console.BufferHeight);
            //    Log.Information(
            //        "--> BufferWidth:  {0,3} --> {1,3}",
            //        w,
            //        Console.BufferWidth);
            //}
            //catch (Exception)
            //{
            Log.Information(
                "--> BufferHeight: {0,3}",
                Console.BufferHeight);
            Log.Information(
                "--> BufferWidth:  {0,3}",
                Console.BufferWidth);
            //}
        }
        #endregion

        #region Methods configuring
        /***********************************************************/
        protected void ConfigureHostConfiguration(
            IConfigurationBuilder builder)
        {
            LogDebug(
                "--> ConfigureHostConfiguration()");

            //builder.AddJsonFile(
            //    GetConfigFile("hostsettings.json"),
            //    optional: true,
            //    reloadOnChange: false);
        }

        protected void ConfigureAppConfiguration(
            HostBuilderContext context,
            IConfigurationBuilder builder)
        {
            LogDebug(
                "--> ConfigureAppConfiguration()");

            IHostEnvironment he = context.HostingEnvironment;

            ApplicationName = he.ApplicationName;
            EnvironmentName = he.EnvironmentName;

            RootPath = Finder.FindDirectory(
                true, he.ContentRootPath);

            ConfPath = Finder.FindDirectory(
                true, he.ContentRootPath, "ABC", "Conf");

            DataPath = Finder.FindDirectory(
                true, he.ContentRootPath, "ABC", "Data");

            var dst = Finder.FindSolutionDirectoryOrThrow(
                he, "DStutz").FullName;

            CodePath = Finder.FindDirectory(
                true, dst, "DStutz.Public", "Coder");

            LogInfo(
                "--> Environment");
            LogInfo(
                "    --> App name:  {0}", ApplicationName);
            LogInfo(
                "    --> Env name:  {0}", EnvironmentName);
            LogInfo(
                "    --> Root path: {0}", RootPath.FullName);
            LogInfo(
                "    --> Conf path: {0}", ConfPath.FullName);
            LogInfo(
                "    --> Data path: {0}", DataPath.FullName);
            LogInfo(
                "    --> Code path: {0}", CodePath.FullName);

            builder.AddJsonFile(
                Finder.FindFile(
                    true,
                    ConfPath.FullName,
                    "appsettings.json").FullName,
                optional: false,
                reloadOnChange: false);

            builder.AddJsonFile(
                Finder.FindFile(
                    false,
                    ConfPath.FullName,
                    "appsettings." + EnvironmentName + ".json").FullName,
                optional: true,
                reloadOnChange: true);

            IConfigurationRoot cr = builder.Build();

            // Read the section Serilog in appsettings.json
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(cr)
                .CreateLogger();

            // Read the sections Info and Admin in appsettings.json
            LogInfo(
                "--> Configuration (using keys)");
            LogInfo(
                "    --> Date:    {0}", cr["Info:Date"]);
            LogInfo(
                "     -> Version: {0}", cr["Info:Version"]);

            var info = new Info();
            var admin = new Admin();

            cr.GetSection("Info").Bind(info);
            cr.GetSection("Admin").Bind(admin);

            LogInfo(
                "--> Configuration (using bind)");
            LogInfo(
                "    --> Date:    {0}", info.Date);
            LogInfo(
                "    --> Version: {0}", info.Version);
            LogInfo(
                "    --> Admin:   {0}", admin.GetSurPreName());
        }

        protected void ConfigureLogging(
            HostBuilderContext context,
            ILoggingBuilder builder)
        {
            LogDebug(
                "--> ConfigureLogging()");
        }
        #endregion

        #region Methods logging
        /***********************************************************/
        protected static void LogDebug(
            string messageTemplate,
            params object[] propertyValues)
        {
            Log.Logger.Debug(messageTemplate, propertyValues);
        }

        protected static void LogInfo(
            string messageTemplate,
            params object[] propertyValues)
        {
            Log.Logger.Information(messageTemplate, propertyValues);
        }
        #endregion
    }
}
