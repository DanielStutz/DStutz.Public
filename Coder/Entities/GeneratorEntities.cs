using DStutz.Apps;
using DStutz.Coder.Entities.Data;
using DStutz.Coder.Entities.EntityBasic;
using DStutz.Coder.Entities.EntityOwned;
using DStutz.Coder.Entities.EntityRelations;

namespace DStutz.Coder.Entities
{
    public class GeneratorEntities : GeneratorBase<JsonEntity>
    {
        #region Constructors
        /***********************************************************/
        public GeneratorEntities(
            IAppContext context)
            : base(context, "Entities")
        { }
        #endregion

        #region Methods loading code files
        /***********************************************************/

        protected override IDictionary<string, FileBase> LoadCodeFilesInt(
            FileInfo file)
        {
            var entities = LoadJsonEntities(file);

            foreach (var pair in entities)
                if (pair.Value.Code.ImportJsonFrom != null)
                    pair.Value.Import(
                        entities[pair.Value.Code.ImportJsonFrom]);

            Dictionary<string, FileBase> dictionary = new();

            foreach (var pair in entities)
                dictionary.Add(pair.Key, GetFileBase(pair.Value));

            return dictionary;
        }

        private FileBase GetFileBase(
            JsonEntity entity)
        {
            if (entity.Owned == true)
                return new FileEntityOwned(
                    new DataEntityOwned(entity));

            if (entity.OwnedProperties != null ||
                entity.Relations1to1 != null ||
                entity.RelationsMto1 != null ||
                entity.Relations1toN != null ||
                entity.RelationsMtoN != null)
                return new FileEntityRelations(
                    new DataEntityRelations(entity));

            return new FileEntityBasic(
                new DataEntityBasic(entity));
        }
        #endregion

        #region Methods checking json entities
        /***********************************************************/
        protected override void CheckJsonEntitiesInt(
            IDictionary<string, JsonEntity> entities)
        {
            var t = new JoinerTableFix(entities.Count + 1, 13);

            int row = 0;
            int col = 0;

            t.SetAlignsRight();
            t.AddColHeader(col++, "File", 'L');
            t.AddColHeader(col++, "Key", 'L');
            t.AddColHeader(col++, "Project", 'L');
            t.AddColHeader(col++, "Abs", 'L');
            t.AddColHeader(col++, "Owd");
            t.AddColHeader(col++, "Keys ");
            t.AddColHeader(col++, "Props");
            t.AddColHeader(col++, "Owned");
            t.AddColHeader(col++, "1:1");
            t.AddColHeader(col++, "M:1");
            t.AddColHeader(col++, "1:N");
            t.AddColHeader(col++, "M:N");
            t.AddColHeader(col++, "Remarks", 'L');

            foreach (var pair in entities)
            {
                row++;
                col = 0;

                var keys = pair.Key.Split("_");
                var entity = pair.Value;

                t.Add(row, col++, keys[0]);
                t.Add(row, col++, keys[1]);
                t.Add(row, col++, GetProject(entity));
                t.Add(row, col++, entity.Abstract);
                t.Add(row, col++, GetStatus(entity.Owned));
                t.Add(row, col++, GetStatus(IsSpecialK, 2, entity.Keys));
                t.Add(row, col++, GetStatus(IsSpecialP, 2, entity.Properties));
                t.Add(row, col++, GetStatus(IsSpecialP, 2, entity.OwnedProperties));
                t.Add(row, col++, entity.Relations1to1);
                t.Add(row, col++, entity.RelationsMto1);
                t.Add(row, col++, entity.Relations1toN);
                t.Add(row, col++, entity.RelationsMtoN);
                t.Add(row, col++, GetContent(entity.Code.Remarks));
            }

            Console.WriteLine(t.ToString());
        }
        #endregion

        #region Functions checking json entities
        /***********************************************************/
        private Func<JsonKey, bool> IsSpecialK = e =>
        {
            return false
                || e.IsOrderBy
                || e.Pseudonym != null
            ;
        };

        private Func<JsonProperty, bool> IsSpecialP = e =>
        {
            return false
                || e.Column != null
            ;
        };

        private string GetProject(
            JsonEntity e)
        {
            if (e.Namespace.StartsWith("DStutz"))
                return "DStutz";

            if (e.Namespace.Contains("Orders"))
                return "Orders";

            if (e.Namespace.Contains("Products"))
                return "Products";

            throw new Exception("Unknown project");
        }
        #endregion
    }
}
