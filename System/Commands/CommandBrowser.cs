namespace DStutz.System.Commands
{
    public enum Browser : int
    {
        Chrome = 0,
        Edge = 1,
        Firefox = 2,
    }

    public class CommandBrowser : Command
    {
        #region Properties
        /***********************************************************/
        private static string[] Browsers { get; } = new string[]
        {
            "chrome.exe",
            "msedge.exe",
            "firefox.exe",
        };
        #endregion

        #region Constructors
        /***********************************************************/
        public CommandBrowser(
            Browser browser)
            : base(Browsers[(int)browser], true) { }
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

        private void OpenLocalFile(
            string? fileDir,
            string fileName)
        {
            var filePath = fileName;

            if (!string.IsNullOrWhiteSpace(fileDir))
                filePath = Path.Combine(fileDir, fileName);

            // TODO Prefix might be different in linux?!
            Handler.Execute(
                Program,
                "file:///" + filePath);
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            OpenLocalFile(
                TestDir,
                "HowTo.html");

            Open("https://github.com");
        }
        #endregion
    }
}
