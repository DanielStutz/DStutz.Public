namespace DStutz.Math.Statistic.Numbers
{
    public class Counter<T>
        where  T: notnull
    {
        #region Fields
        /***********************************************************/
        private IDictionary<T, long> _counters = new SortedDictionary<T, long>();
        #endregion

        #region Constructors
        /***********************************************************/
        public Counter()
        { }
        #endregion

        #region Methods
        /***********************************************************/
        public void Count(T value)
        {
            if (!_counters.ContainsKey(value))
                _counters.Add(value, 0);

            _counters[value]++;
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public void Log(long minValue)
        {
            foreach (var pair in _counters)
                if (pair.Value >= minValue)
                    Console.WriteLine("{0} --> {1}", pair.Key, pair.Value);
        }

        public void Log()
        {
            foreach (var pair in _counters)
                Console.WriteLine("{0} --> {1}", pair.Key, pair.Value);
        }
        #endregion
    }
}
