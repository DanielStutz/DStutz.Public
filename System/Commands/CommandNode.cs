namespace DStutz.System.Commands
{
    public class CommandNode : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandNode()
            : base("node.exe") { }
        #endregion

        #region Methods
        /***********************************************************/
        public void Node(
            DirectoryInfo workingDir,
            string fileNameJS,
            List<string> arguments)
        {
            arguments.Insert(0, fileNameJS);

            Handler.Execute(
                Program,
                arguments,
                workingDir.FullName);

            LogFileAction(fileNameJS, "executed");
        }

        public void Node(
            DirectoryInfo workingDir,
            List<string> arguments)
        {
            Handler.Execute(
                Program,
                arguments,
                workingDir.FullName);

            LogFileAction(arguments[0], "executed");
        }
        #endregion

        #region Methods
        /***********************************************************/
        public void Handlebars(
            DirectoryInfo workingDir,
            string fileNameJS,
            string fileNameHBS,
            string fileNameHTML,
            params string[] fileNamesJSON)
        {
            List<string> arguments = new()
            {
                fileNameJS,
                fileNameHBS,
                fileNameHTML,
            };

            arguments.AddRange(fileNamesJSON);

            Node(
                workingDir,
                arguments);
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            base.Test();

            Handlebars(
                TestDir,
                "hbs-cli.js",
                "People.hbs",
                "People.html",
                "People_01.json",
                "People_02.json"
            );
        }
        #endregion
    }
}
