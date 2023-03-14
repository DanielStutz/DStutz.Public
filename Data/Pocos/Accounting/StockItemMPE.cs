using DStutz.System.Joiners;

using DStutz.Data.Efcos.Accounting;

// Version 1.1.0
namespace DStutz.Data.Pocos.Accounting
{
    public interface IStockItem
    {
        public long Pk1 { get; set; }
        public string SKU { get; set; }
        public DateTime PurchaseDate { get; set; }
        public long PurchaseUnitCent { get; set; }
        public string PurchaseCurrency { get; set; }
        public long PurchaseOrderPk { get; set; }
        public string? SaleChannel { get; set; }
        public DateTime? SaleDate { get; set; }
        public long? SaleUnitCent { get; set; }
        public string? SaleCurrency { get; set; }
        public long? SaleOrderPk { get; set; }
    }

    public class StockItemMPE
        : IPoco<IStockItem>, IStockItem
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string SKU { get; set; }
        public DateTime PurchaseDate { get; set; }
        public long PurchaseUnitCent { get; set; }
        public string PurchaseCurrency { get; set; }
        public long PurchaseOrderPk { get; set; }
        public string? SaleChannel { get; set; }
        public DateTime? SaleDate { get; set; }
        public long? SaleUnitCent { get; set; }
        public string? SaleCurrency { get; set; }
        public long? SaleOrderPk { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return StockItemMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IStockItem, new()
        {
            return StockItemMapper.New.Map<E>(this);
        }
        #endregion
    }
}
