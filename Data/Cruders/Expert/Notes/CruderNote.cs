using DStutz.Data.Efcos.Expert.Notes;
using DStutz.Data.Pocos.Expert.Notes;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DStutz.Data.Cruders.Expert.Notes
{
    public interface ICruderNote
        : ICruder<NoteMPE>
    {
        public Task<List<NoteMPE>> ReadManyByAuthor(long pk);
        public Task<List<NoteMPE>> ReadManyByText(string partialText);
        public Task<List<NoteMPE>> ReadManyByTitle(string partialTitle);
    }

    public class CruderNote
        : CruderPoco<NoteMEE, NoteMPE, INote>, ICruderNote
    {
        #region Constructors
        /***********************************************************/
        public CruderNote(
            DbContext context)
            : base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/

        public async Task<List<NoteMPE>> ReadManyByAuthor(
            long pk)
        {
            return await ReadMany(e =>
                e.AuthorPk1 == pk,
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<NoteMPE>> ReadManyByText(
            string partialText)
        {
            return await ReadMany(e =>
                e.Text.Contains(partialText),
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<NoteMPE>> ReadManyByTitle(
            string partialTitle)
        {
            return await ReadMany(e =>
                e.Title.Contains(partialTitle),
                ICruder.INCLUDE_ALL);
        }
        #endregion

        #region Methods loading
        /***********************************************************/
        protected override NoteMEE Loading(
            EntityEntry<NoteMEE> entry,
            int includeType)
        {
            switch (includeType)
            {
                case ICruder.INCLUDE_ALL:
                    entry.Reference(e => e.Author)
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
