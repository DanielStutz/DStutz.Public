/**********************************************************************
 * The command print.exe works only for *.txt files?!
 **********************************************************************/
namespace DStutz.System.Commands
{
    public class CommandPrint : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandPrint()
            : base("print.exe", false, false, true) { }
        #endregion

        #region Methods
        /***********************************************************/
        public void File(string filePath)
        {
            Handler.Execute(Program, filePath);
        }
        #endregion
    }
}
