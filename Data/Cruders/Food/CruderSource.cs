using DStutz.Data.Efcos.Food;
using DStutz.Data.Pocos.Food;

using Microsoft.EntityFrameworkCore;

namespace DStutz.Data.CRUD.Food
{
    public interface ICruderSource
        : ICruderBLO<SourceMPE>
    {
        //public ValueTask<List<SourceMPE>> ReadManyByName(string partialName);
    }

    public class CruderSource
        //: CruderPoco<SourceMEE, SourceMPE, ISource>, ICruderSource
    {
        #region Constructors
        /***********************************************************/
        public CruderSource(
            DbContext context)
            //: base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        //public async ValueTask<List<SourceMPE>> ReadManyByName(
        //    string partialName)
        //{
        //    return await ReadMany(e =>
        //        e.Name.Contains(partialName),
        //        CInclude.All);
        //}
        #endregion
    }
}
