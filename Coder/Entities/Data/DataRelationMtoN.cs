using DStutz.System.Extensions;

namespace DStutz.Coder.Entities.Data
{
    public class DataRelationMtoN : DataRelationList
    {
        #region Title
        /***********************************************************/
        public static string Title = "Relations m:n (with a junction table)";
        #endregion

        #region Properties owner
        /***********************************************************/
        private DataType OwnerType { get; }
        #endregion

        #region Properties junction
        /***********************************************************/
        private string? JunctionTable { get; }
        private DataType JunctionType { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public DataRelationMtoN(
            DataType ownerType,
            JsonRelationMtoN property)
            : base(property)
        {
            OwnerType = ownerType;
            JunctionTable = property.JunctionTable;
            JunctionType = new DataType(OwnerType, Type, property);

            // List<OwnerRelatedRel>
            AddEfco(JunctionType.N);

            // List<RelPEAny<RelatedMPE, IRelated>>
            AddPoco($"{JunctionType.P}<{Type.PI}>");

            Console.WriteLine("");
            OwnerType.Joiner().WriteRow();
            Type.Joiner().WriteRow();
            JunctionType.Joiner().WriteRow();

            Console.WriteLine("");
            Console.WriteLine(GetProperty(ListTypeEfco, Name, IsOptional));
            Console.WriteLine(GetProperty(ListTypePoco, Name, IsOptional));
        }
        #endregion

        #region Property junction table annotation
        /***********************************************************/
        private string JunctionTableAnnotation
        {
            get
            {
                if (JunctionTable != null)
                    return $"[Table(\"{JunctionTable}\")]";

                return $"[Table(\"{JunctionType.N.TableName()}\")]";
            }
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string[] GetPropertyEfco()
        {
            // public List<OwnerRelatedRel>? WhateverRels { get; set; }
            return new string[] {
                GetProperty(ListTypeEfco, Name, IsOptional),
            };
        }

        public override string[] GetPropertyPoco()
        {
            // public List<RelPEAny<RelatedMPE, IRelated>>? WhateverRels { get; set; }
            return new string[] {
                GetProperty(ListTypePoco, Name, IsOptional),
            };
        }

        public override string[] GetMappingP2E()
        {
            return new string[] {
                $"efco.{Name} =",
                $"    Mapper.{GetMapperMethod()}(",
                $"        poco.{Name},",
                $"        e => e.Map<{JunctionType.N}, {Type.E}>());",
                "",
            };
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
                $"public class {JunctionType.N}",
                $"    : {JunctionType.E}<{OwnerType.E}, {Type.EPI}>",
                "{ }",
            };
        }
        #endregion
    }
}
