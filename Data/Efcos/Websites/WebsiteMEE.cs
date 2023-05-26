using DStutz.Data.Pocos.Websites;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Websites
{
    public abstract class WebsiteMEE
        : IEfco<WebsiteMPE>, IWebsite
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("href")]
        public string Href { get; set; }

        [Column("lang")]
        public string Lang { get; set; }

        [Column("title")]
        public string Title { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return WebsiteMapper.New.Joiner(this); }
        }

        public WebsiteMPE Map()
        {
            return WebsiteMapper.New.Map<WebsiteMPE>(this);
        }
        #endregion
    }

    public class WebsiteMapper
        : IMapper<IWebsite>
    {
        public static WebsiteMapper New { get; } = new WebsiteMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IWebsite e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 80, e1.Href),
                ('L', 2, e1.Lang),
                ('L', 60, e1.Title)
            ).Add(data);
        }

        public E Map<E>(
            IWebsite e1) where E : IWebsite, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Href = e1.Href,
                Lang = e1.Lang,
                Title = e1.Title,
            };
        }
        #endregion
    }
}
