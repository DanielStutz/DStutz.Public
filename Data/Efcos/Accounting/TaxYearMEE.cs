using DStutz.System.Joiners;

using DStutz.Data.Pocos.Accounting;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1
namespace DStutz.Data.Efcos.Accounting
{
    [Table("tax_year")]
    public class TaxYearMEE
        : IEfco<TaxYearMPE>, ITaxYear
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public int Pk1 { get; set; } // Year

        [Column("date_0101")]
        public string Date0101Short { get; set; } // Format MM-dd

        [Column("date_1231")]
        public string Date1231Short { get; set; } // Format MM-dd

        [Column("date_book")]
        public string DateBook { get; set; } // Format yyyy-MM-dd

        [Column("delta_1200 ")]
        public long Delta1200 { get; set; }

        [Column("delta_1201")]
        public long Delta1201 { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return TaxYearMapper.New.Joiner(this);
        }

        public TaxYearMPE Map()
        {
            return TaxYearMapper.New.Map<TaxYearMPE>(this);
        }
        #endregion
    }

    public class TaxYearMapper
        : IMapper<ITaxYear>
    {
        public static TaxYearMapper New { get; } = new TaxYearMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            ITaxYear e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 4, e1.Pk1),
                ('L', 5, e1.Date0101Short),
                ('L', 5, e1.Date1231Short),
                ('L', 10, e1.DateBook),
                ('R', 10, e1.Delta1200),
                ('R', 10, e1.Delta1201),
                ('L', 80, e1.Remark)
            ).Add(data);
        }

        public E Map<E>(
            ITaxYear e1) where E : ITaxYear, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Date0101Short = e1.Date0101Short,
                Date1231Short = e1.Date1231Short,
                DateBook = e1.DateBook,
                Delta1200 = e1.Delta1200,
                Delta1201 = e1.Delta1201,
                Remark = e1.Remark,
            };
        }
        #endregion
    }
}
