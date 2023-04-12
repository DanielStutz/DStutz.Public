namespace DStutz.System.Commands
{
    public class CommandXampp : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandXampp()
            : base("xampp-control.exe") { }
        #endregion

        #region Methods
        /***********************************************************/
        public void StartControlPanel(
            int waitForExitMilliSeconds)
        {
            Handler.WaitForExit = true;
            Handler.WaitForExitMilliSeconds = waitForExitMilliSeconds;
            Handler.Execute(Program);
            Handler.Kill();
        }
        #endregion

        #region Methods handling command options
        /***********************************************************/
        public override string? Help()
        {
            return "Open Xampp-Control-Panel," +
                " go to Config (button top right)" +
                " and check 'Autostart of modules' Apache and MySQL.";
        }

        public override string? Version()
        {
            return Help();
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            StartControlPanel(5000);
        }
        #endregion
    }
}
