using DStutz.Data.Efcos.Websites;

// Version 1.1.0
namespace DStutz.Data.Pocos.Websites
{
    public interface IWebdoc
    {
        public long Pk1 { get; set; }
        public string Href { get; set; }
        public string Lang { get; set; }
        public string Title { get; set; }
        public string Copyright { get; set; }
    }

    public class WebdocMPE
        : IPoco<IWebdoc>, IWebdoc
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Href { get; set; }
        public string Lang { get; set; }
        public string Title { get; set; }
        public string Copyright { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return WebdocMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IWebdoc, new()
        {
            return WebdocMapper.New.Map<E>(this);
        }
        #endregion
    }
}
