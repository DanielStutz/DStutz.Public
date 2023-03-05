using DStutz.System.Extensions;

namespace DStutz.Coder.Entities.Data
{
    public class JsonEntity
    {
        #region Used by all entities
        /***********************************************************/
        public CodeInfoEntity Code { get; set; }
        public string Namespace { get; set; }
        public string Name { get; set; }
        public List<JsonProperty> Properties { get; set; }
        #endregion

        #region Used by owned entities only
        /***********************************************************/
        public bool? Owned { get; set; }
        #endregion

        #region Used by non-owned (basic, relations) entities only
        /***********************************************************/
        public bool Abstract { get; set; } = false;
        public List<JsonKey>? Keys { get; set; }
        public string? Table { get; set; }
        public List<JsonProperty>? OwnedProperties { get; set; }
        public List<JsonProperty>? Relations1to1 { get; set; }
        public List<JsonProperty>? RelationsMto1 { get; set; }
        public List<JsonRelation1toN>? Relations1toN { get; set; }
        public List<JsonRelationMtoN>? RelationsMtoN { get; set; }
        #endregion

        #region Additional properties
        /***********************************************************/
        public bool HasKeyOrderBy
        {
            get
            {
                if (Keys != null)
                    foreach (var key in Keys)
                        if (key.IsOrderBy)
                            return true;

                return false;
            }
        }

        public string TableAnnotation
        {
            get
            {
                if (Table != null)
                    return $"[Table(\"{Table}\")]";

                return $"[Table(\"{Name.TableName()}\")]";
            }
        }
        #endregion
    }
}
