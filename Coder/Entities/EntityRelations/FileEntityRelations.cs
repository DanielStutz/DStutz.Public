using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities.EntityRelations;

public class FileEntityRelations : FileEntity
{
    #region Template
    /***********************************************************/
    private static readonly string Template = @"
REMARKS
using DStutz.Data;

namespace NAMESPACE_BLO;

CURSOR_INT

// Version VERSION
CURSOR_BLO

----

REMARKS
using DStutz.Data;

using NAMESPACE_BLO;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NAMESPACE_DAO;

// Version VERSION
CURSOR_DAO

CURSOR_CLASSES

----

// Version VERSION
CURSOR_CRUDER
";
    #endregion

    #region Constructors
    /***********************************************************/
    public FileEntityRelations(
        DataEntityRelations data)
        : base(Template, data)
    {
        Insert(new CodeInterface(data), "INT", 0);
        Insert(new CodeEntityBLO(data), "BLO", 0);
        Insert(new CodeEntityDAO(data), "DAO", 0);
        Insert(new CodeJunctionClasses(data), "CLASSES", 0);
        //Insert(new CodeMapper(data), "MAPPER", 4);
        PostProcessing();

        //Write(false, false);
    }
    #endregion
}
