using DStutz.Data.Efcos.Expert.Websites;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert.Websites
{
    public interface IArticle
    {
        public long Pk1 { get; set; }
        public string Lang { get; set; }
        public string? Date { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public string? Remark { get; set; }
        public long AuthorPk1 { get; set; }
        public long? SeriesPk1 { get; set; }
    }

    public class ArticleMPE
        : IPoco<IArticle>, IArticle
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Lang { get; set; }
        public string? Date { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Relations 1:n (with default foreign key)
        /***********************************************************/
        public IReadOnlyList<CommentMPE>? Comments { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        public long AuthorPk1 { get; set; }
        public AuthorMPE? Author { get; set; }

        public long? SeriesPk1 { get; set; }
        public SeriesMPE? Series { get; set; }
        #endregion

        #region Relations m:n (with a junction table)
        /***********************************************************/
        public IReadOnlyList<RelPEAny<ProductMPE, IProduct>>? ProductRels { get; set; }
        public IReadOnlyList<RelPEAny<TagMPE, ITag>>? TagRels { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ArticleMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IArticle, new()
        {
            return ArticleMapper.New.Map<E>(this);
        }
        #endregion
    }
}
