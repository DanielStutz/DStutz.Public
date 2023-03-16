using DStutz.System.Exceptions;

namespace DStutz.Data.Pocos.Emails
{
    public class MailProvider
    {
        #region Properties
        /***********************************************************/
        public string Name { get; set; }
        public string Site { get; set; }
        public string Readme { get; set; }
        public IList<MailClient> Clients { get; set; }
        public IList<MailUser> Users { get; set; }
        #endregion

        #region Methods
        /***********************************************************/
        public bool Matches(string userName)
        {
            foreach (var user in Users)
                if (user.UserName.ToLower().Equals(userName.ToLower()))
                    return true;

            return false;
        }

        public MailUser GetMailUser(string userName)
        {
            foreach (var user in Users)
                if (user.UserName.ToLower().Equals(userName.ToLower()))
                    return user;

            throw new NotFoundException(typeof(MailUser), userName);
        }

        public MailClient GetMailClient(string type)
        {
            foreach (var client in Clients)
                if (client.Type.ToUpper().Equals(type.ToUpper()))
                    return client;

            throw new NotFoundException(typeof(MailClient), type);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string ToString()
        {
            return $"{Name}, {Site}";
        }
        #endregion
    }
}
