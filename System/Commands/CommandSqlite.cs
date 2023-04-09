namespace DStutz.System.Commands
{
    public class CommandSqlite : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandSqlite()
            : base("sqlite3") { }
        #endregion

        #region Methods
        /***********************************************************/
        public void Init(
            string workingDir,
            string sqlFile,
            string dbFile)
        {
            var argument = "-init " + sqlFile + " " + dbFile;

            Handler.Execute(Program, argument, workingDir);
            LogFileAction(sqlFile, "loaded");

            try
            {
                File.Delete(
                    Path.Combine(workingDir, sqlFile));
                LogFileAction(sqlFile, "deleted");
            }
            catch (Exception)
            {
                throw new Exception(
                    "Unable to delete file in " + workingDir);
            }
        }
        #endregion
    }
}
