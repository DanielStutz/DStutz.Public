using System.Collections.Generic;

namespace DStutz.System.Commands
{
    public class CommandNode : Command
    {
        /**********************************************************************
         * Constructors
         **********************************************************************/
        public CommandNode()
            : base("node") { }

        /**********************************************************************
         * Methods - Commands 'node'
         **********************************************************************/
        public void Node(
            string workingDir,
            string scriptFile,
            List<string> arguments)
        {
            arguments.Insert(0, scriptFile);
            Handler.Execute(Program, arguments, workingDir);
            LogFileAction(scriptFile, "executed");
        }

        public void Node(
            string workingDir,
            List<string> arguments)
        {
            Handler.Execute(Program, arguments, workingDir);
            LogFileAction(arguments[0], "executed");
        }
    }
}
