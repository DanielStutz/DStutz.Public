namespace DStutz.System.Commands
{
    public class CommandJava : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandJava()
            : base("java.exe") { }
        #endregion

        #region Methods compiling
        /***********************************************************/
        public void Compile(
            DirectoryInfo workingDir,
            string fileNameJAVA)
        {
            Handler.Execute(
                "javac",
                fileNameJAVA,
                workingDir.FullName);
        }
        #endregion

        #region Methods runing
        /***********************************************************/
        public void Run(
            DirectoryInfo workingDir,
            string filenameClassWithoutExtension)
        {
            Handler.Execute(
                Program,
                filenameClassWithoutExtension,
                workingDir.FullName);
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            base.Test();

            Compile(
                TestDir,
                "Whatever.java");

            Run(
                TestDir,
                "Whatever");
        }
        #endregion
    }
}
