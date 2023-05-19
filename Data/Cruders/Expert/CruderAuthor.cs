using DStutz.Data.Efcos.Expert;
using DStutz.Data.Pocos.Expert;

using Microsoft.EntityFrameworkCore;

namespace DStutz.Data.Cruders.Expert
{
    public interface ICruderAuthor
        : ICruder<AuthorMPE>
    {
        public Task<List<AuthorMPE>> ReadManyByPrename(string partialName);
        public Task<List<AuthorMPE>> ReadManyBySurname(string partialName);
        public Task<List<AuthorMPE>> ReadManyByRemark(string partialRemark);
    }

    public class CruderAuthor
        : CruderPoco<AuthorMEE, AuthorMPE, IAuthor>, ICruderAuthor
    {
        #region Constructors
        /***********************************************************/
        public CruderAuthor(
            DbContext context)
            : base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public async Task<List<AuthorMPE>> ReadManyByPrename(
            string partialName)
        {
            return await ReadMany(e =>
                e.Prename.Contains(partialName));
        }

        public async Task<List<AuthorMPE>> ReadManyBySurname(
            string partialName)
        {
            return await ReadMany(e =>
                e.Surname.Contains(partialName));
        }

        public async Task<List<AuthorMPE>> ReadManyByRemark(
            string partialRemark)
        {
            return await ReadMany(e =>
                e.Remark != null &&
                e.Remark.Contains(partialRemark));
        }

        protected async override ValueTask<AuthorMPE> Update(
            AuthorMPE poco,
            bool saveChanges)
        {
            var efco = await FindOrThrow(poco.Pk1);

            efco.Surname = poco.Surname;
            efco.Prename = poco.Prename;
            efco.Href = poco.Href;
            efco.Remark = poco.Remark;

            return (await Update(efco, true)).Map();
        }
        #endregion
    }
}
