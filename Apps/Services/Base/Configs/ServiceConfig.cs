using DStutz.System.Joiners;

namespace DStutz.Apps.Services.Base.Configs
{
    public abstract class ServiceConfig
        : IJoinable
    {
        #region Properties
        /***********************************************************/
        public string ClientId { get; set; }
        public string UniqueId { get; set; }
        public string Remark { get; set; }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public virtual IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    ('L', 30, GetType().Name),
                    ('L', 3, ClientId),
                    ('L', 10, UniqueId)
                );
            }
        }
        #endregion
   }
}
