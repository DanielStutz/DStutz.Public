namespace DStutz.System.Commands
{
    public class CommandTextPad : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandTextPad()
            : base("TextPad.exe", true) { }
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
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            Open(
                TestspaceDir,
                "_ReadMe.txt",
                "CommandPDFLatex.tex",
                "CommandTextPad.txt");
        }
        #endregion
    }
}
