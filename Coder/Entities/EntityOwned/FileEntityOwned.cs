using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities.EntityOwned;

public class FileEntityOwned : FileEntity
{
    #region Template
    /***********************************************************/
    private static readonly string Template = @"
REMARKS
using DStutz.Data;

using NAMESPACE_DAO;

namespace NAMESPACE_BLO;

CURSOR_INT

// Version VERSION
CURSOR_BLO

----

REMARKS
using DStutz.Data;

using NAMESPACE_BLO;

using System.ComponentModel.DataAnnotations.Schema;

namespace NAMESPACE_DAO;

// Version VERSION
CURSOR_DAO

----

// Version VERSION
CURSOR_CRUDER
";
    #endregion

    #region Constructors
    /***********************************************************/
    public FileEntityOwned(
        DataEntityOwned data)
        : base(Template, data)
    {
        Insert(new CodeInterface(data), "INT", 0);
        Insert(new CodeEntityMPE(data), "BLO", 0);
        Insert(new CodeEntityMEE(data), "DAO", 0);
        //Insert(new CodeMapper(data), "MAPPER", 4);
        PostProcessing();

        //Write(false, false);
    }
    #endregion
}
