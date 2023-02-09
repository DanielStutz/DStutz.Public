using DStutz.Apps;
using DStutz.System.IO;

namespace DStutz.Coder
{
    public abstract class GeneratorBase
    {
        #region Properties
        /***********************************************************/
        private IAppContext Context { get; }
        private string JsonType { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public GeneratorBase(
            IAppContext context,
            string jsonType)
        {
            Context = context;
            JsonType = jsonType;
        }
        #endregion

        #region Methods json entities
        /***********************************************************/
        protected IDictionary<string, T> GetDictionary<T>(
            string jsonFile)
        {
            return GetDictionary<T>(JsonType, jsonFile);
        }

        protected IDictionary<string, T> GetDictionary<T>(
            string jsonType,
            string jsonFile)
        {
            var info = Context.GetConfPath($"{jsonType}/{jsonFile}.json");

            //Console.WriteLine(info.FullName);

            if (!info.Exists)
                info = Context.GetJsonFile(jsonType, jsonFile);

            //Console.WriteLine(info.FullName);

            return FileReaderJson.ReadDictionary<string, T>(info);
        }

        protected string GetKey(
            string file,
            string clazz)
        {
            return $"{file}_{clazz}";
        }
        #endregion
    }
}
