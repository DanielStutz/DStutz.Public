using DStutz.Apps;

namespace DStutz.System.Commands
{
    public abstract class Command
    {
        #region Properties
        /***********************************************************/
        public ILogger Logger { get; } = AppLogger.CreateLogger<Command>();
        public string Program { get; }
        public DirectoryInfo TestspaceDir { get; }
        public DirectoryInfo WorkspaceDir { get; }
        protected ProcessHandler Handler { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        private Command(
            string program,
            ProcessHandler handler)
        {
            Program = program;
            Handler = handler;
            TestspaceDir = new DirectoryInfo(@"C:\Workspace\Commands");
            WorkspaceDir = new DirectoryInfo(@"C:\Workspace");
        }

        protected Command(
            string program)
            : this(
                  program,
                  new ProcessHandler())
        { }

        protected Command(
            string program,
            bool useShellExecute)
            : this(
                  program,
                  new ProcessHandler(useShellExecute))
        { }

        protected Command(
            string program,
            bool redirectError,
            bool redirectInput,
            bool redirectOutput)
            : this(
                  program,
                  new ProcessHandler(redirectError, redirectInput, redirectOutput))
        { }
        #endregion

        #region Methods processing
        /***********************************************************/
        public string? GetPath()
        {
            //return Handler.execute("echo", "$env:Path", "");
            // ./write.exe
            // C:\\Windows\\System32
            // C:\\Windows\\SysWOW64
            // C:\\Windows\\Sysnative

            //return Handler.execute("./write", "", "C:\\Windows\\SysWOW64");
            //return Handler.execute("microsoft-edge:");

            // https://stackoverflow.com/questions/39626509/how-to-launch-ms-edge-from-c-sharp-winforms

            return Handler.Execute("microsoft-edge:");
        }

        public string? Execute()
        {
            return Handler.Execute(Program);
        }

        public bool Executed()
        {
            return Handler.GetExitCode() >= 0;
        }
        #endregion

        #region Methods handling command options
        /***********************************************************/
        public string? Help()
        {
            return Handler.Execute(Program, "--help");
        }

        public string? Version()
        {
            return Handler.Execute(Program, "--version");
        }

        #endregion

        #region Methods finding executable programs
        /***********************************************************/
        public string? Where()
        {
            return Handler.Execute("where.exe", Program);
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public virtual void Test()
        {
            throw new NotImplementedException();
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
