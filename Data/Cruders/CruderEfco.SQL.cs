using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DStutz.Data.CRUD
{
    public partial class CruderEfco<E>
    {
        #region Methods reading many entities (sql statement)
        /***********************************************************/
        public async Task<List<EO>> FindMany<EO>(
            string searchFor,
            string ISOCode639 = "de")
            where EO : class
        {
            var name = GetTableName<EO>();
            var code = GetCheckedCode<EO>(ISOCode639);

            FormattableString sql =
                $"SELECT * FROM `{name}` WHERE {code} LIKE '{searchFor}'";

            Console.WriteLine(name);
            Console.WriteLine(code);
            Console.WriteLine(sql.ToString());

            return await Context.Set<EO>()
                .FromSql(sql)
                .ToListAsync();
        }
        #endregion

        #region Methods providing metadata
        /***********************************************************/
        private IEntityType GetEntityType<T>()
        {
            var et = Context.Model.FindEntityType(typeof(T));

            if (et == null)
                throw new Exception(
                    $"Unable to find entity type for {typeof(T).Name}");

            return et;
        }

        private string? GetTableName<T>()
        {
            return GetEntityType<T>().GetTableName();
        }

        private IEnumerable<IProperty> GetProperties<T>()
        {
            return GetEntityType<T>().GetProperties();
        }

        private string GetCheckedCode<T>(
            string ISOCode639 = "de")
        {
            var code = ISOCode639.ToUpper();

            SortedSet<string> set = new();

            foreach (var p in GetProperties<T>())
                if (p.Name.Length == 2)
                    set.Add(p.Name.ToUpper());

            if (set.Contains(code))
                return ISOCode639.ToLower();

            throw new Exception(
                $"{typeof(T).Name} does not contain a property {code}");
        }
        #endregion
    }
}
