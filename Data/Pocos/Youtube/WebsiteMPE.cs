using DStutz.Data.Efcos.Youtube;

// Version 1.1.0
namespace DStutz.Data.Pocos.Youtube
{
    public interface IWebsite
    {
        public long Pk1 { get; set; }
        public string Href { get; set; }
        public string Lang { get; set; }
        public string Title { get; set; }
        public string? Date { get; set; }
        public string? Author { get; set; }
        public string? Remark { get; set; }
    }

    public class WebsiteMPE
        : IPoco<IWebsite>, IWebsite
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Href { get; set; }
        public string Lang { get; set; }
        public string Title { get; set; }
        public string? Date { get; set; }
        public string? Author { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return WebsiteMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IWebsite, new()
        {
            return WebsiteMapper.New.Map<E>(this);
        }
        #endregion
    }
}
