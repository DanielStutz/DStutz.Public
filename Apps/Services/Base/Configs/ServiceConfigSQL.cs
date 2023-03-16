namespace DStutz.Apps.Services.Base.Configs
{
    public abstract class ServiceConfigSQL
        : ServiceConfig
    {
        #region Properties
        /***********************************************************/
        public virtual bool UseMySql { get; } = false;
        public virtual bool UseSqlite { get; } = false;
        public virtual string Type { get; }
        public abstract string Connection { get; }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override IJoiner Joiner
        {
            get
            {
                return base.Joiner.Add(
                    ('L', 10, Type),
                    ('L', 100, Connection)
                );
            }
        }
        #endregion
    }
}
