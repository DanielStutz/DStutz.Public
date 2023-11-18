using DStutz.Data.Efcos.Expert;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert
{
    public interface IProducer
    {
        public long Pk1 { get; set; }
        public string Abbr { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public string Country { get; set; }
    }

    public class ProducerMPE
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Abbr { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public string Country { get; set; }
        #endregion
    }
}
