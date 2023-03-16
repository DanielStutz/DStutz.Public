using DStutz.Data.Efcos.Websites;

// Version 1.1.0
namespace DStutz.Data.Pocos.Websites
{
    public interface IWebdocOby
        : IOrdered
    {
        public long Pk1 { get; set; }
        public string Href { get; set; }
        public string Lang { get; set; }
        public string Title { get; set; }
        public string Copyright { get; set; }
    }

    public class WebdocObyMPE
        : IPoco<IWebdocOby>, IWebdocOby
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public int OrderBy { get; set; }
        public string Href { get; set; }
        public string Lang { get; set; }
        public string Title { get; set; }
        public string Copyright { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return WebdocObyMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IWebdocOby, new()
        {
            return WebdocObyMapper.New.Map<E>(this);
        }
        #endregion
    }
}
