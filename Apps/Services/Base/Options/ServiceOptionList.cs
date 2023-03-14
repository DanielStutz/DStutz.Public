namespace DStutz.Apps.Services.Base.Options
{
    public abstract class ServiceOptionList<S>
        where S : struct
    {
        #region Properties
        /***********************************************************/
        protected List<S> Arguments { get; } = new();
        #endregion

        #region Constructors
        /***********************************************************/
        protected ServiceOptionList()
        { }

        protected ServiceOptionList(
            IEnumerable<S> args)
        {
            foreach (var arg in args)
                Arguments.Add(arg);
        }

        protected ServiceOptionList(
            params S[] args)
        {
            foreach (var arg in args)
                Arguments.Add(arg);
        }
        #endregion
    }
}
