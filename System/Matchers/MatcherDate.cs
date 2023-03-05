using DStutz.Data;

namespace DStutz.System.Matchers
{
    public interface IMatcherDated
    {
        public bool Matches(IDated dated);
    }

    public class MatcherDate : IMatcherDated
    {
        private readonly string Date;

        public MatcherDate(string date)
        {
            Date = date;
        }

        public bool Matches(IDated dated)
        {
            return dated.Date.ToString("yyyy-MM-dd").Equals(Date);
        }
    }

    public class MatcherYear : IMatcherDated
    {
        private readonly int Year;

        public MatcherYear(int year)
        {
            Year = year;
        }

        public bool Matches(IDated dated)
        {
            return dated.Date.Year == Year;
        }
    }

    public class MatcherYearMonth : IMatcherDated
    {
        private readonly int Year;
        private readonly int Month;

        public MatcherYearMonth(int year, int month)
        {
            Year = year;
            Month = month;
        }

        public bool Matches(IDated dated)
        {
            return dated.Date.Year == Year
                && dated.Date.Month == Month;
        }
    }
}
