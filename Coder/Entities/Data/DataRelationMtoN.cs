using DStutz.System.Extensions;

namespace DStutz.Coder.Entities.Data
{
    public class DataRelationMtoN : DataProperty<DataTypeList>
    {
        #region Title
        /***********************************************************/
        public static string Title = "Relations m:n (with a junction table)";
        #endregion

        #region Properties owner and related
        /***********************************************************/
        private DataType OType { get; }
        private DataTypeList RType { get; }
        #endregion

        #region Properties junction
        /***********************************************************/
        private string? Table { get; }
        private DataType JType { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public DataRelationMtoN(
            DataType ownerType,
            JsonRelationMtoN property)
            : base(
                  property,
                  new DataTypeList(property, property.ListType))
        {
            if (property.ListType.Contains("?") ||
                property.JunctionType.Contains("?"))
                throw new JsonOptionalException(
                    "RelationsMtoN",
                    "ListType",
                    "JunctionType");

            // RType is for convenience only
            OType = ownerType;
            RType = Type;
            Table = property.JunctionTable;
            JType = new DataType(OType, RType, property.JunctionType);

            // List<OwnerRelatedRel>
            RType.AddEfco(JType.N);

            // List<RelPEAny<RelatedMPE, IRelated>>
            RType.AddPoco($"{JType.P}<{RType.PI}>");

            Console.WriteLine("");
            OType.Joiner().WriteRow();
            RType.Joiner().WriteRow();
            JType.Joiner().WriteRow();
            Console.WriteLine(GetProperty(RType.LE, Name, IsOptional));
            Console.WriteLine(GetProperty(RType.LP, Name, IsOptional));
        }
        #endregion

        #region Property junction table annotation
        /***********************************************************/
        private string JunctionTableAnnotation
        {
            get
            {
                if (Table != null)
                    return $"[Table(\"{Table}\")]";

                return $"[Table(\"{JType.N.TableName()}\")]";
            }
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string[] GetPropertyEfco()
        {
            // public List<OwnerRelatedRel>? WhateverRels { get; set; }
            return new string[] {
                GetProperty(RType.LE, Name, IsOptional),
            };
        }

        public override string[] GetPropertyPoco()
        {
            // public List<RelPEAny<RelatedMPE, IRelated>>? WhateverRels { get; set; }
            return new string[] {
                GetProperty(RType.LP, Name, IsOptional),
            };
        }

        public override string[] GetMappingP2E()
        {
            return GetMappingP2E(JType.N, RType.E);
        }

        public string[] GetJunctionTableClass()
        {
            // [Table("owner_related_rel")]
            // public class OwnerRelatedRel
            //     : RelEEAny<OwnerMEE, RelatedMEE, RelatedMPE, IRelated>
            // { }
            return new string[] {
                "",
                JunctionTableAnnotation,
                $"public class {JType.N}",
                $"    : {JType.E}<{OType.E}, {RType.EPI}>",
                "{ }",
            };
        }
        #endregion
    }
}
