using DStutz.System.Extensions;

namespace DStutz.Coder.Entities.Data;

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
    private string? JTable { get; }
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
        JTable = property.JunctionTable;
        JType = new DataType(OType, RType, property.JunctionType);

        // List<OwnerRelatedRel>
        RType.AddDAO(JType.N);

        // List<RelBLOAny<RelatedBLO, IRelated>>
        RType.AddBLO($"{JType.B}<{RType.BI}>");

        //Console.WriteLine("");
        //OType.Joiner().WriteRow();
        //RType.Joiner().WriteRow();
        //JType.Joiner().WriteRow();
        //Console.WriteLine(GetProperty(RType.LB, Name, IsOptional));
        //Console.WriteLine(GetProperty(RType.LD, Name, IsOptional));
    }
    #endregion

    #region Property junction table annotation
    /***********************************************************/
    private string JunctionTableAnnotation
    {
        get
        {
            if (JTable != null)
                return $"[Table(\"{JTable}\")]";

            return $"[Table(\"{JType.N.TableName()}\")]";
        }
    }
    #endregion

    #region Miscellaneous
    /***********************************************************/
    public override string[] GetPropertyBLO()
    {
        // public List<RelBLOAny<RelatedPLO, IRelated>>? WhateverRels { get; set; }
        return new string[] {
            GetSetProperty(RType.LB, Name, IsOptional),
        };
    }

    public override string[] GetPropertyDAO()
    {
        // public List<OwnerRelatedRel>? WhateverRels { get; set; }
        return new string[] {
            GetSetProperty(RType.LD, Name, IsOptional),
        };
    }

    public override string[] GetMappingB2D()
    {
        return GetMappingB2D(JType.N, RType.D);
    }

    public string[] GetJunctionTableClass()
    {
        // [Table("owner_related_rel")]
        // public class OwnerRelatedRel
        //     : RelDAOAny<OwnerDAO, RelatedDAO, RelatedBLO, IRelated>
        // { }
        return new string[] {
            "",
            JunctionTableAnnotation,
            $"public class {JType.N}",
            $"    : {JType.D}<{OType.D}, {RType.DBI}>",
            "{ }",
        };
    }
    #endregion
}
