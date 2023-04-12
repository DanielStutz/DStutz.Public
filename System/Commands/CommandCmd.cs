using Google.Protobuf.WellKnownTypes;

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
            // 'echo' is a command inside
            return Handler.Execute(
                Program,
                "/c echo " + value);
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
            // 'set' is a command inside
            Handler.Execute(
                Program,
                "/c set > " + txtFile,
                workingDir.FullName);
        }
        #endregion

        #region Methods printing (very slow and *.txt files only?!)
        /***********************************************************/
        public void Print(
            string txtFile)
        {
            Print(WorkspaceDir, txtFile);
        }

        public void Print(
            DirectoryInfo workingDir,
            string txtFile)
        {
            // Get '\\X1-Carbon-G10\LaserJet'
            var c = Echo("%COMPUTERNAME%");
            var p = "LaserJet";
            var s = @"\\" + c + @"\" + p;

            // 'type' is a command inside
            Handler.Execute(
                Program,
                "/c type " + txtFile + " > " + s,
                workingDir.FullName);
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            SavePath(
                TestDir,
                "EchoPath.txt");

            var paths = EchoPath();

            if (paths != null)
                foreach (var path in paths)
                    Console.WriteLine("--> " + path);

            //Print(
            //    TestDir,
            //    "Paths.txt");
        }
        #endregion
    }
}
