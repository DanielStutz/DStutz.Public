using DStutz.System.Extensions;

namespace DStutz.Coder.Entities.Data
{
    public class JsonEntity
    {
        #region Used by all entities
        /***********************************************************/
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
        public string? Key1 { get; set; }
        public string? Key2 { get; set; }
        public string? Key3 { get; set; }
        public bool OrderBy { get; set; } = false;
        public string? Table { get; set; }
        public List<JsonProperty>? OwnedProperties { get; set; }
        public List<JsonProperty>? Relations1to1 { get; set; }
        public List<JsonProperty>? RelationsMto1 { get; set; }
        public List<JsonRelation1toN>? Relations1toN { get; set; }
        public List<JsonRelationMtoN>? RelationsMtoN { get; set; }
        #endregion

        #region Property table annotation
        /***********************************************************/
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
