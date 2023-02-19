using DStutz.System.Joiners;

using DStutz.Data.Efcos.Accounting;

// Version 1.1
namespace DStutz.Data.Pocos.Accounting
{
    public interface ITaxYear

    {
        public int Pk1 { get; set; } // Year
        public string Date0101Short { get; set; } // Format MM-dd
        public string Date1231Short { get; set; } // Format MM-dd
        public string DateBook { get; set; } // Format yyyy-MM-dd
        public long Delta1200 { get; set; }
        public long Delta1201 { get; set; }
        public string? Remark { get; set; }
    }

    public class TaxYearMPE
        : IPoco<ITaxYear>, ITaxYear
    {
        #region Properties
        /***********************************************************/
        public int Pk1 { get; set; } // Year
        public string Date0101Short { get; set; } // Format MM-dd
        public string Date1231Short { get; set; } // Format MM-dd
        public string DateBook { get; set; } // Format yyyy-MM-dd
        public long Delta1200 { get; set; }
        public long Delta1201 { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Asymmetric code (keys and dates)
        /***********************************************************/
        public int Year { get { return Pk1; } }
        public string Date0101 { get { return $"{Pk1}-{Date0101Short}"; } }
        public string Date1231 { get { return $"{Pk1}-{Date1231Short}"; } }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return TaxYearMapper.New.Joiner(this);
        }

        public E Map<E>() where E : ITaxYear, new()
        {
            return TaxYearMapper.New.Map<E>(this);
        }
        #endregion
    }
}
