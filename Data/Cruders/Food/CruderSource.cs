using DStutz.Data.Efcos.Food;
using DStutz.Data.Pocos.Food;

using Microsoft.EntityFrameworkCore;

namespace DStutz.Data.Cruders.Food
{
    public interface ICruderSource
        : ICruder<SourceMPE>
    {
        public Task<List<SourceMPE>> ReadManyByName(string partialName);
    }

    public class CruderSource
        : CruderPoco<SourceMEE, SourceMPE, ISource>, ICruderSource
    {
        #region Constructors
        /***********************************************************/
        public CruderSource(
            DbContext context)
            : base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public async Task<List<SourceMPE>> ReadManyByName(
            string partialName)
        {
            return await ReadMany(e =>
                e.Name.Contains(partialName));
        }
        #endregion
    }
}
