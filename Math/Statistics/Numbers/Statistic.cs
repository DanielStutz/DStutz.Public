// Licensed ...

using System.Numerics;

namespace DStutz.Math.Statistics.Numbers
{
    public interface IStatistic<T>
        where T : INumber<T>, IMinMaxValue<T>, IDivisionOperators<T, int, double>
    {
        public int Count { get; }
        public T Max { get; }
        public T Min { get; }
        public T Sum { get; }
        public double Average { get; }
        public bool HasNumbers { get; }
        public void Add(T number);
        public void AddRange(IEnumerable<T> numbers);
    }

    public class Statistic<T>
        : IStatistic<T>
        where T : INumber<T>, IMinMaxValue<T>, IDivisionOperators<T, int, double>
    {
        #region Properties
        /***********************************************************/
        public int Count { get; private set; } = 0;
        public T Min { get; private set; } = T.MaxValue;
        public T Max { get; private set; } = T.MinValue;
        public T Sum { get; private set; } = T.Zero;
        #endregion

        #region Properties implementing
        /***********************************************************/
        public double Average
        {
            get { return Count == 0 ? 0.0 : Sum / Count; }
        }

        public bool HasNumbers
        {
            get { return Count > 0; }
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public void Add(
            T number)
        {
            Count++;
            Min = T.Min(Min, number);
            Max = T.Max(Max, number);
            Sum += number;
        }

        public void AddRange(
            IEnumerable<T> numbers)
        {
            foreach (var number in numbers)
                Add(number);
        }
        #endregion
    }
}
