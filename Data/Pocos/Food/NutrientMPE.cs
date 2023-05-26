using DStutz.Data.Efcos.Food;

// Version 1.1.0
namespace DStutz.Data.Pocos.Food
{
    public interface INutrient
        : IDeEnFr
    {
        public long Pk1 { get; set; }
        public string Unit { get; set; }
        public long Group { get; set; }
    }

    public class INutrientMPE
        : IPoco<INutrient>, INutrient, IPolyglot
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string? DE { get; set; }
        public string? EN { get; set; }
        public string? FR { get; set; }
        public string Unit { get; set; }
        public long Group { get; set; }
        #endregion

        #region Asymmetric code
        /***********************************************************/
        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(this, ISOCode639);
        }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return NutrientMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : INutrient, new()
        {
            return NutrientMapper.New.Map<E>(this);
        }
        #endregion
    }
}
