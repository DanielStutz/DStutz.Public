namespace DStutz.System.Commands
{
    public class CommandXampp : Command
    {
        #region Constructors
        /***********************************************************/
        #endregion

        #region Methods
        /***********************************************************/
        #endregion

        #region Miscellaneous
        /***********************************************************/
        #endregion

        /**********************************************************************
         * Constructors
         **********************************************************************/
        public CommandXampp()
            : base(@".\xampp-control.exe", true) { }

        /**********************************************************************
         * Methods
         **********************************************************************/
        public void Start()
        {
            Handler.WaitForExit = true;
            Handler.WaitForExitMilliSeconds = 5000;
            Handler.Execute(Program, "", @"C:\xampp");
            Handler.Kill();
        }
    }
}
