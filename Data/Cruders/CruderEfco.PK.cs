using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DStutz.Data.Cruders
{
    public partial class CruderEfco<E>
    {
        #region Methods getting primary keys
        /***********************************************************/
        private IEnumerable<object?>? FindPrimaryKeysOrDefault(
            EntityEntry<E> entry)
        {
            var key = entry.Metadata.FindPrimaryKey();

            if (key == null)
                return null;

            return key.Properties.Select(p =>
                entry.Property(p.Name).CurrentValue);
        }

        private IEnumerable<object?> FindPrimaryKeysOrThrow(
            EntityEntry<E> entry)
        {
            var key = entry.Metadata.FindPrimaryKey();

            if (key == null)
                throw new Exception("There are no primary keys");

            return key.Properties.Select(p =>
                entry.Property(p.Name).CurrentValue);
        }
        #endregion

        #region Methods getting primary keys for text
        /***********************************************************/
        //public long NextPK<P>(
        //    P entity,
        //    string ISOCode639 = "de")
        //    where P : E, IPolyglot
        //{
        //    return NextPK(entity.FindText(ISOCode639));
        //}

        public long NextPK(
            string text,
            Func<string, long> assigner)
        {
            return NextPK(assigner(text));
        }

        public long NextPK(
            string parsableNumber)
        {
            return NextPK(long.Parse(parsableNumber));
        }

        public long NextPK(
            long pk)
        {
            pk++;

            while (Set.Find(pk) != null)
                pk++;

            return pk;
        }
        #endregion
    }
}
