using DStutz.Data.Efcos.Youtube;
using DStutz.Data.Pocos.Youtube;

using Microsoft.EntityFrameworkCore;

namespace DStutz.Data.Cruders.Youtube
{
    public interface ICruderWebsite
        : ICruder<WebsiteMPE>
    {
        public Task<List<WebsiteMPE>> ReadManyByAuthor(string partialName);
        public Task<List<WebsiteMPE>> ReadManyByRemark(string partialRemark);
    }

    public class CruderWebsite
        : CruderPoco<WebsiteMEE, WebsiteMPE, IWebsite>, ICruderWebsite
    {
        #region Constructors
        /***********************************************************/
        public CruderWebsite(
            DbContext context)
            : base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public async Task<List<WebsiteMPE>> ReadManyByAuthor(
            string partialName)
        {
            return await ReadMany(
                e => e.Author != null && e.Author.Contains(partialName));
        }

        public async Task<List<WebsiteMPE>> ReadManyByRemark(
            string partialRemark)
        {
            return await ReadMany(
                e => e.Remark != null && e.Remark.Contains(partialRemark));
        }
        #endregion
    }
}
