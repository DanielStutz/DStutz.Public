using DStutz.System.Joiners;

using DStutz.Data.Accounting;
using DStutz.Data.Pocos.Accounting;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Accounting
{
    [Table("position")]
    public class PositionMEE
        : IEfco<PositionMPE>, IPosition
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("order_by"), Key]
        public int OrderBy { get; set; }

        [Column("number_payment")]
        public int NumberPayment { get; set; }

        [Column("number_shipment")]
        public int NumberShipment { get; set; }

        [Column("sku")]
        public string SKU { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion

        #region Asymmetric code (database)
        /***********************************************************/
        [Column("currency")]
        public string Currency { get; set; }

        [Column("unitcent")]
        public long UnitCent { get; set; }

        [NotMapped] // Not tested yet!
        public Amount Price
        {
            get
            {
                return new Amount(Currency, UnitCent);
            }

            set
            {
                Currency = value.Currency;
                UnitCent = value.UnitCent;
            }
        }

        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return PositionMapper.New.Joiner(this); }
        }

        public PositionMPE Map()
        {
            return PositionMapper.New.Map<PositionMPE>(this);
        }
        #endregion
    }

    public class PositionMapper
        : IMapper<IPosition>
    {
        public static PositionMapper New { get; } = new PositionMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IPosition e1,
            params IJoinable?[] data)
        {
            var amount = e1.Price; // Additional code!

            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('L', 20, e1.Pk1),
                ('R', 3, e1.OrderBy),
                ('R', 2, e1.NumberPayment),
                ('R', 2, e1.NumberShipment),
                ('L', 20, e1.SKU),
                ('R', 2, e1.Quantity),
                ('L', 3, amount.Currency),
                ('R', 10, amount.UnitCent),
                ('L', 20, e1.Remark)
            ).Add(data);
        }

        public E Map<E>(
            IPosition e1) where E : IPosition, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                OrderBy = e1.OrderBy,
                NumberPayment = e1.NumberPayment,
                NumberShipment = e1.NumberShipment,
                SKU = e1.SKU,
                Quantity = e1.Quantity,
                Remark = e1.Remark,
                Price = e1.Price, // Additional code!
            };
        }
        #endregion
    }
}
