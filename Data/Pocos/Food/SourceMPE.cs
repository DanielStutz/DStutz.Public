using DStutz.Data.Efcos.Food;

// Version 1.1.0
namespace DStutz.Data.Pocos.Food
{
    public interface ISource
    {
        public long Pk1 { get; set; }
        public string Name { get; set; }
    }

    public class SourceMPE
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Name { get; set; }
        #endregion
    }
}
