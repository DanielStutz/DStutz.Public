using DStutz.Apps;
using DStutz.System.IO;

namespace DStutz.Coder
{
    public abstract class GeneratorBase<T>
    {
        #region Properties
        /***********************************************************/
        private IAppContext Context { get; }
        private string JsonType { get; }
        private SortedDictionary<string, FileInfo> Files1 { get; } = new();
        private SortedDictionary<string, FileInfo> Files2 { get; } = new();
        #endregion

        #region Constructors
        /***********************************************************/
        public GeneratorBase(
            IAppContext context,
            string jsonType)
        {
            Context = context;
            JsonType = jsonType;


            var dir1 = context.GetConfPath($"{jsonType}");
            Console.WriteLine(dir1);

            //foreach (var file in dir1.EnumerateFiles())
            //    Files2.Add(file.Name, file);

            // Code files in DStutz
            //var dir2 = context.GetCodePath($"{jsonType}/Json");

            //foreach (var file in dir2.EnumerateFiles())
            //    Files2.Add(file.Name, file);

            //foreach (var file in Files2.Values)
            //    Console.WriteLine(file.Name);
        }
        #endregion

        #region Methods code files
        /***********************************************************/
        public FileBase GetCodeFile(
            string file,
            string key)
        {
            return GetFileBases(file)[GetKey(file, key)];
        }

        public ICollection<FileBase> GetCodeFiles(
            string file)
        {
            return GetFileBases(file).Values;
        }

        public ICollection<FileBase> GetCodeFiles()
        {

            return null;
        }

        public void SafeAndOpenCodeFile(
            string file,
            string key)
        {
            GetCodeFile(file, key).SafeAndOpenWithTextPad();
        }

        public void SafeAndOpenCodeFiles(
            string file)
        {
            foreach (var item in GetCodeFiles(file))
                item.SafeAndOpenWithTextPad();
        }
        #endregion

        #region Methods file bases
        /***********************************************************/
        private IDictionary<string, FileBase> GetFileBases(
            params string[] files)
        {
            IDictionary<string, FileBase> data =
                new Dictionary<string, FileBase>();

            foreach (var file in files)
                foreach (KeyValuePair<string, T> pair in
                    GetDictionary(file))
                    data.Add(
                        GetKey(file, pair.Key),
                        GetFileBase(pair.Value));

            return data;
        }

        protected abstract FileBase GetFileBase(T entity);
        #endregion

        #region Methods json objects
        /***********************************************************/
        private IDictionary<string, T> GetDictionary(
            string jsonFile)
        {
            return GetDictionary(JsonType, jsonFile);
        }

        private IDictionary<string, T> GetDictionary(
            string jsonType,
            string jsonFile)
        {
            return null;
            //return FileReaderJson.ReadDictionary<string, T>(
            //    Context.GetJsonFile(jsonType, jsonFile));
        }

        private string GetKey(
            string file,
            string clazz)
        {
            return $"{file}_{clazz}";
        }
        #endregion
    }
}
