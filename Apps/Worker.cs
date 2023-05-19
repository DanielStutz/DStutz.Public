namespace DStutz.Apps
{
    public interface IWorker
    {
        public IAppContext Context { get; }
        public ILogger Logger { get; }
    }

    public abstract class Worker
        : IWorker
    {
        #region Properties
        /***********************************************************/
        public IAppContext Context { get; }
        public ILogger Logger { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected Worker(
            IAppContext appContext)
        {
            Context = appContext;
            Logger = Context.CreateLogger(this);

            AppLogger.LogStart(this);
        }
        #endregion
    }
}
