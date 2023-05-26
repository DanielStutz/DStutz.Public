using static DStutz.Coder.Entities.Data.DataType;

namespace DStutz.Coder.Entities.Data;

public class DataEntityBasic : DataEntity
{
    #region Properties
    /***********************************************************/
    public bool AbstractBLO { get; } = false;
    public bool AbstractDAO { get; } = false;
    public bool IsOrderBy { get; }
    public string TableAnnotation { get; }
    public string Keys { get; }
    #endregion

    #region Constructors
    /***********************************************************/
    public DataEntityBasic(
        JsonEntity entity)
    : base(entity, BLO, DAO)
    {
        if (entity.Abstract != null)
        {
            AbstractBLO = entity.Abstract.Contains("B");
            AbstractDAO = entity.Abstract.Contains("D");
        }

        IsOrderBy = entity.HasOrderByKey;
        TableAnnotation = entity.TableAnnotation;

        int i = 1;

        // Handle keys
        if (entity.Keys != null)
            foreach (var key in entity.Keys)
                if (key.IsOrderBy)
                    Properties.Add(
                        new DataPropertyColumn(key));
                else
                    Properties.Add(
                        new DataPropertyColumn(key, i++));

        if (Properties != null)
            Keys = string.Join(", ", Properties.Select(e => e.Name));

        // Handle other properties
        if (entity.Properties != null)
            foreach (var property in entity.Properties)
                Properties.Add(
                    new DataPropertyColumn(property));
    }
    #endregion
}
