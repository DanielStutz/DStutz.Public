using DStutz.Data.Efcos.Food;

// Version 1.1.0
namespace DStutz.Data.Pocos.Food
{
    public interface INutrientData
    {
        public long Pk1 { get; set; }
        public string Value { get; set; }
        public string? Derivation { get; set; }
        public string? Sources { get; set; }
    }

    public class NutrientDataMPE
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public int OrderBy { get; set; }
        public string Value { get; set; }
        public string? Derivation { get; set; }
        public string? Sources { get; set; }
        #endregion
    }
}
