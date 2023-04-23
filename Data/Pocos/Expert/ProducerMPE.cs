using DStutz.Data.Efcos.Expert;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert
{
    public interface IProducer
    {
        public long Pk1 { get; set; }
        public string Abbr { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
    }

    public class ProducerMPE
        : IPoco<IProducer>, IProducer
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Abbr { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ProducerMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IProducer, new()
        {
            return ProducerMapper.New.Map<E>(this);
        }
        #endregion
    }
}
