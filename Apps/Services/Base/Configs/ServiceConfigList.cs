using DStutz.System.IO;

namespace DStutz.Apps.Services.Base.Configs
{
    public class ServiceConfigList<T>
        : ServiceConfig
    {
        #region Properties
        /***********************************************************/
        public List<T> List { get; } = new();
        #endregion

        #region Constructors
        /***********************************************************/
        public ServiceConfigList(
            FileInfo? info = null)
        {
            if (info != null)
                List = FileReaderJson.ReadList<T>(info);
        }
        #endregion
    }
}
