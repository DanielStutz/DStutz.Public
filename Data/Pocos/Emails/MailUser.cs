namespace DStutz.Data.Pocos.Emails
{
    public enum UserRole
    {
        A = 'A', // Admin
        M = 'M', // Messenger
        N = 'N', // Nobody
        Z = 'Z'  // Zombie
    }

    public class MailUser
    {
        #region Properties
        /***********************************************************/
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; } = UserRole.N;
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string ToString()
        {
            return $"{UserRole} {UserName}";
        }
        #endregion
    }
}
