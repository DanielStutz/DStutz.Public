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
        public List<JsonProperty>? Properties { get; set; }
        #endregion

        #region Used by owned entities only
        /***********************************************************/
        public bool? Owned { get; set; }
        #endregion

        #region Used by non-owned (basic, relations) entities only
        /***********************************************************/
        public string? Abstract { get; set; } // Use 'E' or 'E,P'
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
        public bool HasOrderByKey
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

        #region Methods importing data
        /***********************************************************/
        public void Import(
            JsonEntity other)
        {
            Keys = Prepend(Keys, other.Keys);
            Properties = Prepend(Properties, other.Properties);
            OwnedProperties = Prepend(OwnedProperties, other.OwnedProperties);
            Relations1to1 = Prepend(Relations1to1, other.Relations1to1);
            RelationsMto1 = Prepend(RelationsMto1, other.RelationsMto1);
            Relations1toN = Prepend(Relations1toN, other.Relations1toN);
            RelationsMtoN = Prepend(RelationsMtoN, other.RelationsMtoN);
        }

        public List<T>? Append<T>(
            List<T>? listOfThis,
            List<T>? listOfOther)
        {
            if (listOfThis == null)
                return listOfOther;

            if (listOfOther != null)
                foreach (var item in listOfOther)
                    listOfThis.Add(item);

            return listOfThis;
        }

        public List<T>? Prepend<T>(
            List<T>? listOfThis,
            List<T>? listOfOther)
        {
            if (listOfThis == null)
                return listOfOther;

            int index = 0;

            if (listOfOther != null)
                foreach (var item in listOfOther)
                    listOfThis.Insert(index++, item);

            return listOfThis;
        }
        #endregion
    }
}
