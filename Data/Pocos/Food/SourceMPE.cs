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
        : IPoco<ISource>, ISource
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Name { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        [JsonIgnore]
        public IJoiner Joiner
        {
            get { return SourceMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : ISource, new()
        {
            return SourceMapper.New.Map<E>(this);
        }
        #endregion
    }
}
