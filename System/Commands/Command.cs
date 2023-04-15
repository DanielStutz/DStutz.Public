using DStutz.Apps;

namespace DStutz.System.Commands
{
    public abstract class Command
    {
        #region Properties
        /***********************************************************/
        public ILogger Logger { get; } = AppLogger.CreateLogger<Command>();
        public string Program { get; }
        public IEnumerable<FileInfo> Executables { get; }
        public DirectoryInfo WorkspaceDir { get; }
        public DirectoryInfo TestspaceDir { get; }
        protected DirectoryInfo TestDir { get; set; }
        protected ProcessHandler Handler { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        private Command(
            string fileName,
            ProcessHandler handler)
        {
            Program = fileName;
            Handler = handler;

            WorkspaceDir =
                new DirectoryInfo(
                    @"C:\Workspace");

            TestspaceDir =
                new DirectoryInfo(
                    @"C:\Workspace\Commands");

            TestDir =
                new DirectoryInfo(
                    @"C:\Workspace\Commands\" + GetType().Name);
        }

        protected Command(
            string fileName)
            : this(
                  fileName,
                  new ProcessHandler())
        {
            Executables = Locate(fileName);
        }

        protected Command(
            string fileName,
            string software)
            : this(
                  fileName,
                  new ProcessHandler())
        {
            Executables = Locate(fileName, software);
        }

        protected Command(
            string fileName,
            bool useShellExecute)
        : this(
                  fileName,
                  new ProcessHandler(useShellExecute))
        { }

        protected Command(
            string fileName,
            bool redirectError,
            bool redirectInput,
            bool redirectOutput)
            : this(
                  fileName,
                  new ProcessHandler(redirectError, redirectInput, redirectOutput))
        { }
        #endregion

        #region Properties additional
        /***********************************************************/
        public string? Where
        {
            get
            {
                return Handler.Execute("where.exe", Program);
            }
        }

        public string[] Paths
        {
            get
            {
                var path = Environment.GetEnvironmentVariable("PATH");

                if (path == null)
                    throw new ArgumentNullException("PATH");

                var paths = path.Split(Path.PathSeparator);

                // Remove any trailing '\'
                for (int i = 0; i < paths.Length; i++)
                    if (paths[i].EndsWith(Path.DirectorySeparatorChar))
                        paths[i] = paths[i].Remove(paths[i].Length - 1, 1);

                return paths;
            }
        }

        #endregion

        #region Methods processing
        /***********************************************************/
        public string? Execute()
        {
            return Handler.Execute(Program);
        }

        public bool Executed()
        {
            return Handler.GetExitCode() >= 0;
        }
        #endregion

        #region Methods handling command line options
        /***********************************************************/
        public virtual string? Help()
        {
            return Handler.Execute(Program, "--help");
        }

        public virtual string? Version()
        {
            return Handler.Execute(Program, "--version");
        }
        #endregion

        #region Methods finding executable programs
        /***********************************************************/
        public IEnumerable<FileInfo> Locate()
        {
            return Locate(Program);
        }

        public IEnumerable<FileInfo> Locate(
            string fileName)
        {
            return Locate(
                 fileName,
                 fileName.Replace(".exe", "").ToLower()
            );
        }

        public IEnumerable<FileInfo> Locate(
            string fileName,
            string software)
        {
            var files = new List<FileInfo>();

            foreach (var path in Paths)
            {
                if (path.ToLower().Contains(software))
                {
                    var full = Path.Combine(path, fileName);

                    if (File.Exists(full))
                        files.Add(new FileInfo(full));
                    //else
                    //    Console.WriteLine("ER " + full);
                }
            }

            //foreach (var file in files)
            //    Console.WriteLine("OK " + file.FullName);

            return files;
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public virtual void Test()
        {
            Version();
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public void SetLogAnyway()
        {
            Handler.LogAnyway = true;
        }

        protected void LogFileAction(string file, string action)
        {
            Logger.LogInformation(
                "--> File {0} has been {1}",
                file,
                action);
        }
        #endregion
    }
}
