using DStutz.Data.Pocos.Emails;
using DStutz.System.Joiners;

namespace DStutz.Apps.Services.Base.Configs
{
    public class ServiceConfigEmails
        : ServiceConfig
    {
        #region Properties
        /***********************************************************/
        public string Name { get; set; }
        public MailClient ClientIMAP { get; set; }
        public MailClient ClientSMTP { get; set; }
        public ICollection<MailUser> Users { get; set; }
        public string FolderLogging { get; set; }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override IJoiner Joiner
        {
            get
            {
                return base.Joiner.Add(
                    ('L', 20, Name)
                );
            }
        }
        #endregion
    }
}
