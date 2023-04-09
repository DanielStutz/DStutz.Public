namespace DStutz.System.Commands
{
    public class CommandCmd : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandCmd()
            : base("cmd.exe", false, false, true) { }
        #endregion

        #region Methods echoing
        /***********************************************************/
        public string? Echo(
            string value)
        {
            return Handler.Execute(
                Program,
                "/c echo " + value); // echo is a command inside
        }

        public string[]? EchoPath()
        {
            var v = Echo("%Path%");

            if (v != null)
                return v.Split(";");

            return null;
        }

        public string? EchoProgramFiles()
        {
            var v = Echo("%ProgramFiles%");

            if (v != null)
                return v;

            return null;
        }
        #endregion

        #region Methods saving
        /***********************************************************/
        public void SavePath(
            DirectoryInfo workingDir,
            string txtFile)
        {
            Handler.Execute(
                Program,
                "/c set > " + txtFile, // set is a command inside
                workingDir.FullName);
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            SavePath(
                TestspaceDir,
                "CommandCmd.txt");

            var paths = EchoPath();

            if (paths != null)
                foreach (var path in paths)
                    Console.WriteLine("--> " + path);
        }
        #endregion
    }
}
