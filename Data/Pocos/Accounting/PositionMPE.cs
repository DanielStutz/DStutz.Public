using DStutz.Data.Accounting;
using DStutz.Data.Efcos.Accounting;

using System.Text.Json.Serialization;

// Version 1.1.0
namespace DStutz.Data.Pocos.Accounting
{
    public interface IPosition
        : IOrdered
    {
        public long Pk1 { get; set; }
        public int NumberPayment { get; set; }
        public int NumberShipment { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public string? Remark { get; set; }
        public Amount Price { get; set; } // Additional code!
    }

    public class PositionMPE
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        [JsonPropertyName("Number")] public int OrderBy { get; set; }
        public int NumberPayment { get; set; }
        public int NumberShipment { get; set; }
        [JsonPropertyName("Sku")] public string SKU { get; set; }
        public int Quantity { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Asymmetric code (json files)
        /***********************************************************/
        public Amount PriceUnit { get; set; }
        public Amount PriceTotal { get; set; }
        #endregion
        public Amount Price { get; set; }

        #region Miscellaneous
        /***********************************************************/
        public string GetText()
        {
            return Quantity == 1 ? SKU : SKU + " * " + Quantity;
        }
        #endregion
    }
}
