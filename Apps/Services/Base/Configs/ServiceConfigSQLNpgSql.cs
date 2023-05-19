namespace DStutz.Apps.Services.Base.Configs
{
    public class ServiceConfigSQLNpgSql
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
        public override bool UseNpgSql { get; } = true;
        public override string Type { get; } = "NpgSql";
        public override string Connection
        {
            get
            {
                return "Host=" + Host + ";" +
                    (string.IsNullOrWhiteSpace(Port) ? "" : "Port=" + Port + ";") +
                    "Username=" + Username + ";" +
                    (string.IsNullOrWhiteSpace(Password) ? "" : "Password=" + Password + ";") +
                    "Database=" + Database;
            }
        }
        #endregion
    }
}
