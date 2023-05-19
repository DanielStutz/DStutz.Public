using DStutz.Data.Efcos.Expert;
using DStutz.Data.Efcos.Expert.Websites;
using DStutz.Data.Pocos.Expert;
using DStutz.Data.Pocos.Expert.Websites;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DStutz.Data.Cruders.Expert.Websites
{
    public interface ICruderArticle
        : ICruder<ArticleMPE>
    {
        public Task<List<ArticleMPE>> ReadManyByAuthor(long pk);
        public Task<List<ArticleMPE>> ReadManyByProduct(string partialProduct);
        public Task<List<ArticleMPE>> ReadManyByRemark(string partialRemark);
        public Task<List<ArticleMPE>> ReadManyBySeries(long pk);
        public Task<List<ArticleMPE>> ReadManyByTag(string partialTag);
        public Task<List<ArticleMPE>> ReadManyByTitle(string partialTitle);
        public Task<List<TagMPE>> ReadManyTags(string partialTag, string ISOCode639 = "de");
    }

    public class CruderArticle
        : CruderPoco<ArticleMEE, ArticleMPE, IArticle>, ICruderArticle
    {
        #region Constructors
        /***********************************************************/
        public CruderArticle(
            DbContext context)
            : base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/

        public async Task<List<ArticleMPE>> ReadManyByAuthor(
            long pk)
        {
            return await ReadMany(e =>
                e.AuthorPk1 == pk,
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<ArticleMPE>> ReadManyByProduct(
            string partialProduct)
        {
            return await ReadMany<ArticleProductRel>(e =>
                e.Related != null && (
                    e.Related.Name.Contains(partialProduct) ||
                    e.Related.Type.Contains(partialProduct)
                ),
                ICruder.INCLUDE_ALL
            );
        }

        public async Task<List<ArticleMPE>> ReadManyByRemark(
            string partialRemark)
        {
            return await ReadMany(e =>
                e.Remark != null &&
                e.Remark.Contains(partialRemark),
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<ArticleMPE>> ReadManyBySeries(
            long pk)
        {
            return await ReadMany(e =>
                e.SeriesPk1 == pk,
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<ArticleMPE>> ReadManyByTag(
            string partialTag)
        {
            return await ReadMany<ArticleTagRel>(e =>
                e.Related != null && (
                    (e.Related.DE != null &&
                    e.Related.DE.Contains(partialTag)) ||
                    (e.Related.EN != null &&
                    e.Related.EN.Contains(partialTag)) ||
                    (e.Related.FR != null &&
                    e.Related.FR.Contains(partialTag))
                ),
                ICruder.INCLUDE_ALL
            );
        }

        public async Task<List<ArticleMPE>> ReadManyByTitle(
            string partialTitle)
        {
            return await ReadMany(e =>
                e.Title.Contains(partialTitle),
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<TagMPE>> ReadManyTags(
            string partialTag,
            string ISOCode639 = "de")
        {
            return await ReadMany<TagMEE, TagMPE, ITag>(partialTag, ISOCode639);
        }

        //public async Task<List<ArticleMEE>> ReadManyByHref(
        //    string partialHref)
        //{
        //    // OK
        //    //return await Set
        //    //    .FromSql($"SELECT * FROM `website_article`")
        //    //    .ToListAsync();

        //    // OK
        //    // FindArticlesByHref (IN SearchFor VARCHAR(60))
        //    // SELECT * FROM `website_article` WHERE `href` LIKE SearchFor
        //    return await Set
        //        .FromSql($"CALL FindArticlesByHref({partialHref})")
        //        .ToListAsync();

        //    // OK
        //    // FindArticlesByAuthor(IN Pk1 BIGINT)
        //    // SELECT * FROM `website_article` WHERE `author_pk1` = Pk1
        //    //return await Set
        //    //    .FromSql($"CALL FindArticlesByAuthor({2})")
        //    //    .ToListAsync();
        //}
        #endregion

        #region Methods loading
        /***********************************************************/
        protected override ArticleMEE Loading(
            EntityEntry<ArticleMEE> entry,
            int includeType)
        {
            switch (includeType)
            {
                case ICruder.INCLUDE_ALL:
                    entry.Reference(e => e.Author)
                        .Load();
                    entry.Collection(e => e.Comments)
                        .Load();
                    entry.Collection(e => e.ProductRels).Query()
                        .Include(r => r.Related!.Producer)
                        .Load();
                    entry.Reference(e => e.Series)
                        .Load();
                    entry.Collection(e => e.TagRels).Query()
                        .Include(r => r.Related)
                        .Load();
                    break;
                default:
                    break;
            }

            return entry.Entity;
        }
        #endregion
    }
}
