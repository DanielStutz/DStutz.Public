namespace DStutz.System.Commands
{
    public class CommandJava : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandJava()
            : base("java") { }
        #endregion

        #region Methods compiling
        /***********************************************************/
        public void Compile(
            DirectoryInfo workingDir,
            string javaFile)
        {
            Handler.Execute(
                "javac",
                javaFile,
                workingDir.FullName);
        }
        #endregion

        #region Methods runing
        /***********************************************************/
        public void Run(
            DirectoryInfo workingDir,
            string classFile)
        {
            Handler.Execute(
                Program,
                classFile,
                workingDir.FullName);
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            Compile(
                TestspaceDir,
                "CommandJava.java");

            Run(
                TestspaceDir,
                "CommandJava");
        }
        #endregion
    }
}
