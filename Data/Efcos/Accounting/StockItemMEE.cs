using DStutz.Data.Pocos.Accounting;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Accounting
{
    [Table("stock_item")]
    public class StockItemMEE
        : IEfco<StockItemMPE>, IStockItem
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("sku")]
        public string SKU { get; set; }

        [Column("in_date")]
        public DateTime PurchaseDate { get; set; }

        [Column("in_unitcent")]
        public long PurchaseUnitCent { get; set; }

        [Column("in_currency")]
        public string PurchaseCurrency { get; set; }

        [Column("in_order_pk1")]
        public long PurchaseOrderPk { get; set; }

        [Column("out_channel")]
        public string? SaleChannel { get; set; }

        [Column("out_date")]
        public DateTime? SaleDate { get; set; }

        [Column("out_unitcent")]
        public long? SaleUnitCent { get; set; }

        [Column("out_currency")]
        public string? SaleCurrency { get; set; }

        [Column("out_order_pk1")]
        public long? SaleOrderPk { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return StockItemMapper.New.Joiner(this); }
        }

        public StockItemMPE Map()
        {
            return StockItemMapper.New.Map<StockItemMPE>(this);
        }
        #endregion
    }

    public class StockItemMapper
        : IMapper<IStockItem>
    {
        public static StockItemMapper New { get; } = new StockItemMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IStockItem e1,
            params IJoinableOld?[] data)
        {
            var j = new Joiner(
                //('L', 20, e1.GetType().Name),
                ('L', 18, e1.SKU),
                ('R', 10, e1.PurchaseDate.ToShortDateString()),
                ('L', 3, e1.PurchaseCurrency),
                ('R', 6, e1.PurchaseUnitCent)
            );

            if (e1.SaleDate != null)
                j.Add(
                    ('L', 3, e1.SaleChannel),
                    ('R', 10, ((DateTime)e1.SaleDate).ToShortDateString()),
                    ('L', 3, e1.SaleCurrency),
                    ('R', 6, e1.SaleUnitCent)
                );

            return j;
        }

        public E Map<E>(
            IStockItem e1) where E : IStockItem, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                SKU = e1.SKU,
                PurchaseDate = e1.PurchaseDate,
                PurchaseUnitCent = e1.PurchaseUnitCent,
                PurchaseCurrency = e1.PurchaseCurrency,
                PurchaseOrderPk = e1.PurchaseOrderPk,
                SaleChannel = e1.SaleChannel,
                SaleDate = e1.SaleDate,
                SaleUnitCent = e1.SaleUnitCent,
                SaleCurrency = e1.SaleCurrency,
                SaleOrderPk = e1.SaleOrderPk,
            };
        }
        #endregion
    }
}
