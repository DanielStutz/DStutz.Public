namespace DStutz.Apps.Services.Base.Options
{
    public abstract class ServiceOptionListInt
    {
        #region Properties
        /***********************************************************/
        protected List<int> Arguments { get; } = new();
        #endregion

        #region Constructors
        /***********************************************************/
        protected ServiceOptionListInt()
        { }

        protected ServiceOptionListInt(
            IEnumerable<int> args)
        {
            foreach (var arg in args)
                Arguments.Add(arg);
        }

        protected ServiceOptionListInt(
            params int[] args)
        {
            foreach (var arg in args)
                Arguments.Add(arg);
        }

        protected ServiceOptionListInt(
            int lower,
            int upper)
        {
            for (int i = lower; i <= upper; i++)
                Arguments.Add(i);
        }
        #endregion
    }
}
