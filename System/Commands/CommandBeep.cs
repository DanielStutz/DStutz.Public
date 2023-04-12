namespace DStutz.System.Commands
{
    public class CommandBeep : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandBeep()
            : base("cmd.exe", false, false, true) { }
        #endregion

        #region Methods
        /***********************************************************/
        public string Beep()
        {
            // Use cmd.exe as echo is not an executable but a command inside

            // Does NOT work!
            //return Handler.Execute(Program, "/c echo `a");

            // Does NOT work!
            return Handler.Execute(Program, "/c echo ^G");

            // Does NOT work!
            //return Handler.Execute(Program, "/c echo 0x07");
        }
        #endregion
    }
}
