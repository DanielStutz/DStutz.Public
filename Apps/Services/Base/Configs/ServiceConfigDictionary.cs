using DStutz.System.IO;

namespace DStutz.Apps.Services.Base.Configs
{
    public class ServiceConfigDictionary<K, V>
        : ServiceConfig
        where K : notnull
    {
        #region Properties
        /***********************************************************/
        public Dictionary<K, V> Dictionary { get; } = new();
        #endregion

        #region Constructors
        /***********************************************************/
        public ServiceConfigDictionary(
            FileInfo? info = null)
        {
            if (info != null)
                Dictionary = FileReaderJson.ReadDictionary<K, V>(info);
        }
        #endregion
    }
}
