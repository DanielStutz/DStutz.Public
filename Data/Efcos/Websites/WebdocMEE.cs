using DStutz.Data.Pocos.Websites;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Websites
{
    public abstract class WebdocMEE
        : IEfco<WebdocMPE>, IWebdoc
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

        [Column("copyright")]
        public string Copyright { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return WebdocMapper.New.Joiner(this); }
        }

        public WebdocMPE Map()
        {
            return WebdocMapper.New.Map<WebdocMPE>(this);
        }
        #endregion
    }

    public class WebdocMapper
        : IMapper<IWebdoc>
    {
        public static WebdocMapper New { get; } = new WebdocMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IWebdoc e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 80, e1.Href),
                ('L', 2, e1.Lang),
                ('L', 60, e1.Title),
                ('L', 20, e1.Copyright)
            ).AddOLD(data);
        }

        public E Map<E>(
            IWebdoc e1) where E : IWebdoc, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Href = e1.Href,
                Lang = e1.Lang,
                Title = e1.Title,
                Copyright = e1.Copyright,
            };
        }
        #endregion
    }
}
