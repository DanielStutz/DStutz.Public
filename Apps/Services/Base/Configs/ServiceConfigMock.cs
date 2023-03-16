namespace DStutz.Apps.Services.Base.Configs
{
    public class ServiceConfigMock
        : ServiceConfig
    {
        #region Constructors
        /***********************************************************/
        public ServiceConfigMock()
        {
            ClientId = "APP";
            Remark = "No config read from file!";
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override IJoiner Joiner
        {
            get
            {
                return base.Joiner.Add(
                    ('L', 30, Remark)
                );
            }
        }
        #endregion
    }
}
