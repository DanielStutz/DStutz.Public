using DStutz.Data.Efcos.Food;

// Version 1.1.0
namespace DStutz.Data.Pocos.Food
{
    public interface INutrientData
        : IOrdered
    {
        public long Pk1 { get; set; }
        public string Value { get; set; }
        public string? Derivation { get; set; }
        public string? Sources { get; set; }
    }

    public class NutrientDataMPE

        : IPoco<INutrientData>, INutrientData
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public int OrderBy { get; set; }
        public string Value { get; set; }
        public string? Derivation { get; set; }
        public string? Sources { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        [JsonIgnore]
        public IJoiner Joiner
        {
            get { return NutrientDataMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : INutrientData, new()
        {
            return NutrientDataMapper.New.Map<E>(this);
        }
        #endregion
    }
}
