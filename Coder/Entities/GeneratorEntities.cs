using DStutz.Apps;
using DStutz.Coder.Entities.Data;
using DStutz.Coder.Entities.EntityBasic;
using DStutz.Coder.Entities.EntityOwned;
using DStutz.Coder.Entities.EntityRelations;

namespace DStutz.Coder.Entities
{
    public class GeneratorEntities : GeneratorBase
    {
        #region Constructors
        /***********************************************************/
        public GeneratorEntities(
            IAppContext context)
            : base(context, "Entities")
        { }
        #endregion

        #region Methods file entities
        /***********************************************************/
        public FileBase GetEntity(
            string file,
            string clazz)
        {
            return GetEntityClasses(file)[GetKey(file, clazz)];
        }

        public ICollection<FileBase> GetEntities(
            string file)
        {
            return GetEntityClasses(file).Values;
        }

        private IDictionary<string, FileBase> GetEntityClasses(
            params string[] files)
        {
            IDictionary<string, FileBase> data =
                new Dictionary<string, FileBase>();

            foreach (var file in files)
                foreach (KeyValuePair<string, JsonEntity> pair in
                    GetDictionary<JsonEntity>(file))
                    data.Add(
                        GetKey(file, pair.Key),
                        GetDataEntity(pair.Value));

            return data;
        }

        private FileBase GetDataEntity(
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
    }
}
