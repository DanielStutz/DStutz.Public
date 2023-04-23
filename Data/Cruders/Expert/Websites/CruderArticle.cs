using DStutz.Data.Efcos.Expert.Websites;
using DStutz.Data.Pocos.Expert.Websites;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DStutz.Data.Cruders.Expert.Websites
{
    public interface ICruderArticle
        : ICruder<ArticleMPE>
    {
        public Task<List<ArticleMPE>> ReadManyByAuthor(string partialName);
        public Task<List<ArticleMPE>> ReadManyByProduct(string partialProduct);
        public Task<List<ArticleMPE>> ReadManyByTag(string partialTag);
        public Task<List<ArticleMPE>> ReadManyByRemark(string partialRemark);
        public Task<List<ArticleMPE>> ReadManyBySeries(long pk);
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
            string partialName)
        {
            return await ReadMany(e =>
                e.Author != null &&
                e.Author.Contains(partialName),
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<ArticleMPE>> ReadManyByProduct(
            string partialProduct)
        {
            var keys =
                FindOwnerPrimaryKeys<ArticleProductRel>(e =>
                    e.Related != null && (
                        (e.Related.Name != null &&
                        e.Related.Name.Contains(partialProduct)) ||
                        (e.Related.Type != null &&
                        e.Related.Type.Contains(partialProduct))
                    )
                );

            return await ReadMany(e =>
                keys.Contains(e.Pk1),
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<ArticleMPE>> ReadManyByTag(
            string partialTag)
        {
            var keys =
                FindOwnerPrimaryKeys<ArticleTagRel>(e =>
                    e.Related != null && (
                        (e.Related.DE != null &&
                        e.Related.DE.Contains(partialTag)) ||
                        (e.Related.EN != null &&
                        e.Related.EN.Contains(partialTag)) ||
                        (e.Related.FR != null &&
                        e.Related.FR.Contains(partialTag))
                    )
                );

            return await ReadMany(e =>
                keys.Contains(e.Pk1),
                ICruder.INCLUDE_ALL);
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
        #endregion

        #region Methods loading
        /***********************************************************/
        protected override ArticleMEE Loading(
            int includeType,
            EntityEntry<ArticleMEE> entry)
        {
            switch (includeType)
            {
                case ICruder.INCLUDE_ALL:
                    entry.Collection(e => e.Comments)
                        .Load();
                    entry.Collection(e => e.ProductRels).Query()
                        .Include(r => r.Related!.Producer)
                        .Load();
                    entry.Collection(e => e.TagRels).Query()
                        .Include(r => r.Related)
                        .Load();
                    entry.Reference(e => e.Series)
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
