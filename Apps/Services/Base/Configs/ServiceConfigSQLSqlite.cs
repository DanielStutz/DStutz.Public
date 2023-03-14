namespace DStutz.Apps.Services.Base.Configs
{
    public class ServiceConfigSQLSqlite : ServiceConfigSQL
    {
        #region Properties
        /***********************************************************/
        public string Folder { get; set; }
        public string Database { get; set; }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public override bool UseSqlite { get; } = true;
        public override string Type { get; } = "Sqlite";
        public override string Connection
        {
            get
            {
                return "Data Source=" + Folder + "/" + Database;
            }
        }
        #endregion
    }
}
