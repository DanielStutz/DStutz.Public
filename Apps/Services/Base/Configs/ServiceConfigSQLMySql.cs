namespace DStutz.Apps.Services.Base.Configs
{
    public class ServiceConfigSQLMySql
        : ServiceConfigSQL
    {
        #region Properties
        /***********************************************************/
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public override bool UseMySql { get; } = true;
        public override string Type { get; } = "MySql";
        public override string Connection
        {
            get
            {
                return "server=" + Host + ";" +
                    (string.IsNullOrWhiteSpace(Port) ? "" : "port=" + Port + ";") +
                    "uid=" + Username + ";" +
                    (string.IsNullOrWhiteSpace(Password) ? "" : "pwd=" + Password + ";") +
                    "database=" + Database;
            }
        }
        #endregion
    }
}
