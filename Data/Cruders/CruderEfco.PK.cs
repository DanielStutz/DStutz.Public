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
        public long NextPK<P>(
            P entity,
            string ISOCode639 = "de")
            where P : E, IPolyglot
        {
            return NextPK(entity.FindText(ISOCode639));
        }

        public long NextPK(
            string text)
        {
            var pk1 = PK.Assign14D5Z(text) + 1;

            while (Set.Find(pk1) != null)
                pk1++;

            return pk1;
        }
        #endregion
    }
}
