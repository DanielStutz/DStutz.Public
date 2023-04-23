using DStutz.Data.Pocos.Expert;
using DStutz.Data.Pocos.Expert.Websites;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert.Websites
{
    [Table("website_article")]
    public class ArticleMEE
        : IEfco<ArticleMPE>, IArticle
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("lang")]
        public string Lang { get; set; }

        [Column("date")]
        public string? Date { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("href")]
        public string Href { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }

        [Column("author")]
        public string? Author { get; set; }
        #endregion

        #region Relations 1:n (with default foreign key)
        /***********************************************************/
        [ForeignKey("Pk1")]
        public IReadOnlyList<CommentMEE>? Comments { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [Column("series_pk1")]
        public long? SeriesPk1 { get; set; }

        [ForeignKey("SeriesPk1")]
        public SeriesMEE? Series { get; set; }
        #endregion

        #region Relations m:n (with a junction table)
        /***********************************************************/
        public IReadOnlyList<ArticleProductRel>? ProductRels { get; set; }
        public IReadOnlyList<ArticleTagRel>? TagRels { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ArticleMapper.New.Joiner(this); }
        }

        public ArticleMPE Map()
        {
            return ArticleMapper.New.Map<ArticleMPE>(this);
        }
        #endregion
    }

    [Table("website_article_product_rel")]
    public class ArticleProductRel
        : RelEEAny<ArticleMEE, ProductMEE, ProductMPE, IProduct>
    { }

    [Table("website_article_tag_rel")]
    public class ArticleTagRel
        : RelEEAny<ArticleMEE, TagMEE, TagMPE, ITag>
    { }

    public class ArticleMapper
        : IMapper<IArticle>
    {
        public static ArticleMapper New { get; } = new ArticleMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IArticle e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 2, e1.Lang),
                ('L', 10, e1.Date),
                ('L', 60, e1.Title),
                ('L', 80, e1.Href),
                ('L', 40, e1.Remark),
                ('L', 40, e1.Author),
                ('L', 20, e1.SeriesPk1)
            ).Add(data);
        }

        public E Map<E>(
            IArticle e1) where E : IArticle, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                Lang = e1.Lang,
                Date = e1.Date,
                Title = e1.Title,
                Href = e1.Href,
                Remark = e1.Remark,
                Author = e1.Author,
                SeriesPk1 = e1.SeriesPk1,
            };

            if (typeof(E) == typeof(ArticleMEE))
            {
                ArticleMPE poco = (ArticleMPE)(object)e1;
                ArticleMEE efco = (ArticleMEE)(object)e2;

                efco.Comments =
                    Mapper.MapOptionals(
                        poco.Comments,
                        e => e.Map<CommentMEE>());

                efco.Series =
                    Mapper.MapOptional(
                        poco.Series,
                        e => e.Map<SeriesMEE>());

                efco.ProductRels =
                    Mapper.MapOptionals(
                        poco.ProductRels,
                        e => e.Map<ArticleProductRel, ProductMEE>());

                efco.TagRels =
                    Mapper.MapOptionals(
                        poco.TagRels,
                        e => e.Map<ArticleTagRel, TagMEE>());
            }
            else if (typeof(E) == typeof(ArticleMPE))
            {
                ArticleMEE efco = (ArticleMEE)(object)e1;
                ArticleMPE poco = (ArticleMPE)(object)e2;

                poco.Comments =
                    Mapper.MapOptionals(
                        efco.Comments,
                        e => e.Map());

                poco.Series =
                    Mapper.MapOptional(
                        efco.Series,
                        e => e.Map());

                poco.ProductRels =
                    Mapper.MapOptionals(
                        efco.ProductRels,
                        e => e.Map());

                poco.TagRels =
                    Mapper.MapOptionals(
                        efco.TagRels,
                        e => e.Map());
            }
            else
            {
                throw new NotImplementedException();
            }

            return e2;
        }
        #endregion
    }
}