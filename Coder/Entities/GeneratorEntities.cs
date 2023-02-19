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

        #region Methods file bases
        /***********************************************************/
        protected override FileBase GetFileBase(
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
