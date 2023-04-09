/**********************************************************************
 * Use cmd.exe as echo is not an executable but a command inside
 **********************************************************************/
namespace DStutz.System.Commands
{
    public class CommandBeep : Command
    {
        /**********************************************************************
         * Constructors
         **********************************************************************/
        public CommandBeep()
            : base("cmd.exe", false, false, true) { }

        /**********************************************************************
         * Methods
         **********************************************************************/
        public string Beep()
        {
            // Does NOT work!
            //return Handler.Execute(Program, "/c echo `a");

            // Does NOT work!
            return Handler.Execute(Program, "/c echo ^G");

            // Does NOT work!
            //return Handler.Execute(Program, "/c echo 0x07");
        }
    }
}
