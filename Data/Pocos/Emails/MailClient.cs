namespace DStutz.Data.Pocos.Emails
{
    public class MailClient
    {
        #region Properties
        /***********************************************************/
        public string Type { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string ToString()
        {
            return $"{Type} {UseSSL} {Host}:{Port}";
        }
        #endregion
    }
}
