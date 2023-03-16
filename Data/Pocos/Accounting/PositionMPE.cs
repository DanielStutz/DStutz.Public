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
        : IPoco<IPosition>, IPosition
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

        [JsonIgnore] // Not tested yet!
        public Amount Price
        {
            get
            {
                if (PriceUnit != null && PriceTotal == null)
                    return new Amount(
                        PriceUnit.Currency,
                        PriceUnit.UnitCent * Quantity);

                if (PriceUnit == null && PriceTotal != null)
                    return PriceTotal;

                throw new Exception(
                    "Either price per unit or total price must be set");
            }

            set
            {
                PriceTotal = value;
            }
        }

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return PositionMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IPosition, new()
        {
            return PositionMapper.New.Map<E>(this);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public string GetText()
        {
            return Quantity == 1 ? SKU : SKU + " * " + Quantity;
        }
        #endregion
    }
}
