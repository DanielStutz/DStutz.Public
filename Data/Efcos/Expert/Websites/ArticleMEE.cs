using DStutz.Data.Pocos.Expert;
using DStutz.Data.Pocos.Expert.Websites;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert.Websites
{
    [Table("website_article")]
    public class ArticleMEE
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
        #endregion

        //#region Relations 1:n (with default foreign key)
        ///***********************************************************/
        //[ForeignKey("Pk1")]
        //public IReadOnlyList<ArticleComment>? Comments { get; set; }
        //#endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [Column("author_pk1")]
        public long AuthorPk1 { get; set; }

        [ForeignKey("AuthorPk1")]
        public AuthorMEE? Author { get; set; }

        [Column("series_pk1")]
        public long? SeriesPk1 { get; set; }

        [ForeignKey("SeriesPk1")]
        public SeriesMEE? Series { get; set; }
        #endregion

        //#region Relations m:n (with a junction table)
        ///***********************************************************/
        //public IReadOnlyList<ArticleProductRel>? ProductRels { get; set; }
        //public IReadOnlyList<ArticleTagRel>? TagRels { get; set; }
        //#endregion
    }

    //[Table("website_article_comment")]
    //public class ArticleComment
    //: CommentMEE
    //{ }

    //[Table("website_article_product_rel")]
    //public class ArticleProductRel
    //    : RelEEAny<ArticleMEE, ProductMEE, ProductMPE, IProduct>
    //{ }

    //[Table("website_article_tag_rel")]
    //public class ArticleTagRel
    //    : RelEEAny<ArticleMEE, TagMEE, TagMPE, ITag>
    //{ }
}
