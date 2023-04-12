namespace DStutz.System.Commands
{
    public class CommandTextPad : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandTextPad()
            : base("TextPad.exe") { }
        #endregion

        #region Methods opening
        /***********************************************************/
        public void Open(
            string filePath)
        {
            Open(WorkspaceDir, filePath);
        }

        public void Open(
            DirectoryInfo workingDir,
            string filePath)
        {
            Handler.Execute(
                Program,
                filePath,
                workingDir.FullName);

            // TODO ?!
            //Handler.Kill();
        }

        public void Open(
            params string[] filePaths)
        {
            Open(WorkspaceDir, filePaths);
        }

        public void Open(
            DirectoryInfo workingDir,
            params string[] filePaths)
        {
            Handler.Execute(
                Program,
                filePaths,
                workingDir.FullName);

            // TODO ?!
            //Handler.Kill();
        }
        #endregion

        #region Methods opening
        /***********************************************************/
        public void OpenHorizontallySplitted(
            DirectoryInfo workingDir,
            string filePath1,
            string filePath2)
        {
            OpenSplitted(workingDir, "-ah", filePath1, filePath2);
        }

        public void OpenVerticallySplitted(
            DirectoryInfo workingDir,
            string filePath1,
            string filePath2)
        {
            OpenSplitted(workingDir, "-av", filePath1, filePath2);
        }

        private void OpenSplitted(
            DirectoryInfo workingDir,
            string splitType,
            string filePath1,
            string filePath2)
        {
            // Start a new instance of TextPad with '-m'
            List<string> arguments = new()
            {
                "-m",
                splitType,
                filePath1,
                filePath2
            };

            Handler.Execute(
                Program,
                arguments,
                workingDir.FullName);
        }
        #endregion

        #region Methods handling command options
        /***********************************************************/
        public override string? Help()
        {
            return "Open TextPad, go to 'Help --> Help Topics' and" +
                " type 'command line parameters' to see existing" +
                " command line parameters like '-ah', '-av' or '-m'.";
        }

        public override string? Version()
        {
            return Help();
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            Open(
                TestDir,
                "../_ReadMe.txt",
                "TextPadConfig.txt",
                "TextPadPowerShell.txt");

            //OpenHorizontallySplitted(
            //    TestDir,
            //    "File_Left.txt",
            //    "File_Right.txt");

            //OpenVerticallySplitted(
            //    TestDir,
            //    "File_Left.txt",
            //    "File_Right.txt");
        }
        #endregion
    }
}
