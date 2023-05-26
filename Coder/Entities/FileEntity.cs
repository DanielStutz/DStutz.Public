using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities;

public class FileEntity : FileBase
{
    #region Properties
    /***********************************************************/
    private DataEntity Data { get; }
    #endregion

    #region Constructors
    /***********************************************************/
    public FileEntity(
        string codeTemplate,
        DataEntity data)
        : base(
              data.Name,
              data.GetType().Name.Replace("Data", ""),
              codeTemplate,
              data.Code,
              "1.2.0")
    {
        Data = data;
    }
    #endregion

    #region Methods
    /***********************************************************/
    protected override void PostProcessing()
    {
        base.PostProcessing();

        if (Data.Namespace.StartsWith("DStutz.Data"))
            Replace("using DStutz.Data;", "");

        Replace("NAMESPACE_BLO", Data.GetNamespaceBLO());
        Replace("NAMESPACE_DAO", Data.GetNamespaceDAO());
        Replace("TYPE_INT", Data.Type.I);
        Replace("TYPE_BLO", Data.Type.B);
        Replace("TYPE_DAO", Data.Type.D);
        Replace("TYPE_CRUD", Data.Type.C);
    }
    #endregion
}
