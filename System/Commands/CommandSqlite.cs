namespace DStutz.System.Commands
{
    public class CommandSqlite : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandSqlite()
            : base(@"sqlite3.exe", "sqlite") { }
        #endregion

        #region Methods
        /***********************************************************/
        public void Init(
            DirectoryInfo workingDir,
            string fileNameSQL,
            string fileNameDB,
            bool removeSqlFile = false)
        {
            // See https://www.sqlite.org/cli.html
            var argument = "-init " + fileNameSQL + " " + fileNameDB;

            Handler.Execute(
                Program,
                argument,
                workingDir.FullName);

            LogFileAction(
                fileNameSQL,
                "loaded");

            try
            {
                if (removeSqlFile)
                {
                    File.Delete(
                        Path.Combine(
                            workingDir.FullName,
                            fileNameSQL));

                    LogFileAction(
                        fileNameSQL,
                        "deleted");
                }
            }
            catch (Exception)
            {
                throw new Exception(
                    "Unable to delete file in " +
                    workingDir.FullName);
            }
        }
        #endregion

        #region Methods handling command options
        /***********************************************************/
        public override string? Help()
        {
            return "See https://www.sqlite.org/cli.html for cli options.";
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
            //base.Test();

            Init(
                TestDir,
                "People.sql",
                "People.db");
        }
        #endregion
    }
}
