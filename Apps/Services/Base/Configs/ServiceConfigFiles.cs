using DStutz.System.Joiners;

namespace DStutz.Apps.Services.Base.Configs
{
    public class ServiceConfigFiles
        : ServiceConfig
    {
        #region Properties
        /***********************************************************/
        public string[] Folders { get; set; }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override IJoiner Joiner
        {
            get
            {
                return base.Joiner.Add(
                    ('L', 100, string.Join(", ", Folders))
                );
            }
        }
        #endregion
    }
}
