using System.Text;

namespace DStutz.Apps.Services.Base.Configs
{
    public class ServiceConfigAPI
        : ServiceConfig
    {
        #region Properties
        /***********************************************************/
        public string Name { get; set; }
        public string Host { get; set; }
        public string Uri { get; set; }
        public bool OpenAPI { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IDictionary<string, string> RequestHeaders { get; set; }
        #endregion

        #region Methods
        /***********************************************************/
        public bool HasAuthentication()
        {
            return Username != null && Password != null;
        }

        public string GetAuthentication()
        {
            return Convert.ToBase64String(
                Encoding.UTF8.GetBytes(Username + ":" + Password));
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override IJoiner Joiner
        {
            get
            {
                return base.Joiner.Add(
                    ('L', 20, Name),
                    ('L', 40, Uri)
                );
            }
        }
        #endregion
    }
}
