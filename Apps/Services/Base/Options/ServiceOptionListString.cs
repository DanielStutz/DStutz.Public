namespace DStutz.Apps.Services.Base.Options
{
    public abstract class ServiceOptionListString
    {
        #region Properties
        /***********************************************************/
        protected List<string> Arguments { get; } = new();
        #endregion

        #region Constructors
        /***********************************************************/
        protected ServiceOptionListString()
        { }

        protected ServiceOptionListString(
            IEnumerable<int> args)
        {
            foreach (var arg in args)
                Arguments.Add(arg.ToString());
        }

        protected ServiceOptionListString(
            params int[] args)
        {
            foreach (var arg in args)
                Arguments.Add(arg.ToString());
        }

        protected ServiceOptionListString(
            int lower,
            int upper)
        {
            for (int i = lower; i <= upper; i++)
                Arguments.Add(i.ToString());
        }

        protected ServiceOptionListString(
            IEnumerable<string> args)
        {
            foreach (var arg in args)
                Arguments.Add(arg);
        }

        protected ServiceOptionListString(
            params string[] args)
        {
            foreach (var arg in args)
                Arguments.Add(arg);
        }
        #endregion
    }
}
