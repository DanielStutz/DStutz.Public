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
            FileInfo file)
        {
            var entities = LoadJsonEntities(file);

            Console.WriteLine(
                $"{file.Name} ({entities.Count()} entities)");

            foreach (var pair in entities)
            {
                var entity = pair.Value;

                var hit = false
                    //|| HasKeyWithPseudonym(entity)
                    || HasOldProperties(entity)
                    //|| HasRelations(entity)
                    //|| IsPublicCode(entity)
                    //|| !entity.Code.Version.Equals("1.1.0")
                    ;

                if (hit)
                {
                    Console.WriteLine("    Y --> " + pair.Key);
                }
                else
                {
                    Console.WriteLine("    N --> " + pair.Key);
                }
            }
        }
        #endregion

        #region Functions checking json entities
        /***********************************************************/
        private Func<JsonEntity, bool> HasKeyWithPseudonym =
            e =>
            {
                if (e.Keys != null)
                    foreach (var key in e.Keys)
                        if (key.Pseudonym != null)
                            return true;

                return false;
            };

        private Func<JsonEntity, bool> HasOldProperties =
            e => e.Version != null ||
                 e.Warning != null ||
                 e.Comment != null ||
                 e.Remarks != null ||
                 e.AsymmetricCode ||
                 e.OrderBy;

        private Func<JsonEntity, bool> HasRelations =
            e => false
                 || e.Relations1to1 != null
                 //|| e.Relations1toN != null
                 //|| e.RelationsMto1 != null
                 //|| e.RelationsMtoN != null
                 ;

        private Func<JsonEntity, bool> IsPublicCode =
            e => e.Namespace.StartsWith("DStutz");
        #endregion
    }
}
