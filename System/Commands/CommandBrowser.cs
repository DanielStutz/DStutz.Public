using DStutz.Coder;

namespace DStutz.System.Commands
{
    public class CommandChrome : CommandBrowser
    {
        public CommandChrome()
            : base("chrome.exe", true) { }
    }

    public class CommandEdge : CommandBrowser
    {
        public CommandEdge()
            : base("msedge.exe", true) { }
    }

    public class CommandFirefox : CommandBrowser
    {
        public CommandFirefox()
            : base("firefox.exe", true) { }
    }

    public abstract class CommandBrowser : Command
    {
        #region Constructors
        /***********************************************************/
        protected CommandBrowser(
            string programm,
            bool useShellExecute)
            : base(
                  programm,
                  useShellExecute)
        { }
        #endregion

        #region Methods opening
        /***********************************************************/
        public void Open(
            string href)
        {
            Handler.Execute(
                Program,
                href);
        }

        public void OpenLocalFile(
            FileInfo filePath)
        {
            OpenLocalFile(filePath.DirectoryName, filePath.Name);
        }

        public void OpenLocalFile(
            DirectoryInfo fileDir,
            string fileName)
        {
            OpenLocalFile(fileDir.FullName, fileName);
        }

        public void OpenLocalFile(
            string? fileDir,
            string fileName)
        {
            var filePath = fileName;

            if (!string.IsNullOrWhiteSpace(fileDir))
                filePath = Path.Combine(fileDir, fileName);

            // TODO Prefix might be different in linux?!
            filePath = "file:///" + filePath;

            Handler.Execute(
                Program,
                filePath);
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            OpenLocalFile(
                TestspaceDir,
                "CommandBrowser.html");

            Open("https://github.com");
        }
        #endregion
    }
}
