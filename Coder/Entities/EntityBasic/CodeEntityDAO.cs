using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities.EntityBasic;

public class CodeEntityDAO : CodeBlock
{
    #region Template
    /***********************************************************/
    private static readonly string Template = @"
TABLE
public ABSTRACT class TYPE_DAO
    : IEntityDAO<TYPE_INT>, TYPE_INT
{
CURSOR_PROPERTIES
CURSOR_ASYMMETRIC_CODE

    #region Methods implementing
    /***********************************************************/
    public void Update(
        TYPE_INT entity)
    {
CURSOR_ASSIGNS
    }
    #endregion
}";
    #endregion

    #region Constructors
    /***********************************************************/
    public CodeEntityDAO(
        DataEntityBasic entity)
        : base(Template)
    {
        if (entity.AbstractDAO)
        {
            Replace("TABLE", "");
            Replace("ABSTRACT", "abstract");
        }
        else
        {
            Replace("TABLE", entity.TableAnnotation);
            Replace("ABSTRACT ", "");
        }

        if (entity.Code.Asymmetric)
            InsertRegionAsymmetricCode(4);

        // Simple properties
        SetCursor("PROPERTIES", 4)
            .InsertRegion(
                DataPropertyColumn.Title,
                entity.Properties,
                e => e.GetPropertyDAO());

        // Simple properties
        SetCursor("ASSIGNS", 8)
            .Insert(
                entity.Properties,
                e => e.GetAssign());

        //Write(false, false);
    }
    #endregion
}
