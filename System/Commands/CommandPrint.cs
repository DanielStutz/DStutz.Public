namespace DStutz.System.Commands
{
    public class CommandPrint : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandPrint()
            : base("print.exe", false, false, true) { }
        #endregion

        #region Methods handling command options
        /***********************************************************/
        public override string? Help()
        {
            return Handler.Execute(Program, "/?");
        }
        #endregion

        #region Methods printing (very slow and *.txt files only?!)
        /***********************************************************/
        public void File(
            string filePath)
        {
            File(WorkspaceDir, filePath);
        }

        public void File(
            DirectoryInfo workingDir,
            string filePath)
        {
            // TODO This does NOT work !!!
            Handler.Execute(
                Program,
                filePath,
                workingDir.FullName);
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            File(
                TestspaceDir,
                "_ReadMe.txt");
        }
        #endregion
    }
}
