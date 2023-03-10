using DStutz.Apps;
using DStutz.System.IO;

namespace DStutz.Coder
{
    public abstract class GeneratorBase<T>
    {
        #region Properties
        /***********************************************************/
        protected IAppContext Context { get; }
        protected ILogger Logger { get; }
        private IDictionary<string, FileInfo> Files1 { get; }
        private IDictionary<string, FileInfo> Files2 { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected GeneratorBase(
            IAppContext context,
            string jsonType)
        {
            Context = context;
            Logger = Context.CreateLogger(this);

            // Load folder '[Project]/ABC/Conf/[jsonType]'
            var dir1 = context.AppConfig.GetConfDir($"{jsonType}");
            Files1 = Load(dir1);

            // Load folder 'DStutz/Coder/[jsonType]/Json'
            var dir2 = context.AppConfig.GetCodeDir($"{jsonType}/Json");
            Files2 = Load(dir2);
        }

        #endregion

        #region Methods json files
        /***********************************************************/
        private IDictionary<string, FileInfo> Load(
            DirectoryInfo dir)
        {
            Logger.LogInformation("Loading dir {0}", dir.FullName);

            SortedDictionary<string, FileInfo> files = new();

            foreach (var file in dir.EnumerateFiles())
            {
                Logger.LogInformation("    --> {0}", file.Name);
                files.Add(GetJsonFileKey(file.Name), file);
            }

            return files;
        }

        private FileInfo GetJsonFileInfo(
            string fileKey)
        {
            var key = GetJsonFileKey(fileKey);

            if (Files1.ContainsKey(key))
                return Files1[key];

            if (Files2.ContainsKey(key))
                return Files2[key];

            throw new Exception(
                $"Unable to find file for key '{key}'");
        }

        public virtual string GetJsonFileKey(
            string file)
        {
            return file.Replace(".json", "");
        }
        #endregion

        #region Methods code files
        /***********************************************************/
        public void SaveAndOpenCodeFile(
            string fileKey,
            string entityKey)
        {
            GetCodeFile(fileKey, entityKey).SaveAndOpenWithTextPad();
        }

        public FileBase GetCodeFile(
            string fileKey,
            string entityKey)
        {
            return LoadCodeFiles(fileKey)[entityKey];
        }

        public void SaveAndOpenCodeFiles(
            params string[] fileKeys)
        {
            foreach (var item in GetCodeFiles(fileKeys))
                item.SaveAndOpenWithTextPad();
        }

        public ICollection<FileBase> GetCodeFiles(
            params string[] fileKeys)
        {
            Dictionary<string, FileBase> dictionary = new();

            foreach (var fileKey in fileKeys)
                foreach (var pair in LoadCodeFiles(fileKey))
                    dictionary.Add(pair.Key, pair.Value);

            return dictionary.Values;
        }
        #endregion

        #region Methods loading code files
        /***********************************************************/
        public IDictionary<string, FileBase> LoadCodeFiles(
            string fileKey)
        {
            return LoadCodeFilesInt(GetJsonFileInfo(fileKey));
        }

        public IDictionary<string, FileBase> LoadCodeFiles(
            FileInfo file)
        {
            return LoadCodeFilesInt(Check(file));
        }

        protected abstract IDictionary<string, FileBase> LoadCodeFilesInt(
            FileInfo file);
        #endregion

        #region Methods checking json entities
        /***********************************************************/
        public void CheckJsonEntities()
        {
            foreach (var file in Files1.Values)
                CheckJsonEntitiesInt(file);

            foreach (var file in Files2.Values)
                CheckJsonEntitiesInt(file);
        }

        public void CheckJsonEntities(
            string fileKey)
        {
            CheckJsonEntitiesInt(GetJsonFileInfo(fileKey));
        }

        public void CheckJsonEntities(
            FileInfo file)
        {
            CheckJsonEntitiesInt(Check(file));
        }

        protected abstract void CheckJsonEntitiesInt(
            FileInfo file);

        #endregion

        #region Methods loading json entities
        /***********************************************************/
        public IDictionary<string, T> LoadJsonEntities(
            string fileKey)
        {
            return LoadJsonEntities(GetJsonFileInfo(fileKey));
        }

        public IDictionary<string, T> LoadJsonEntities(
            FileInfo file)
        {
            return FileReaderJson.ReadDictionary<string, T>(Check(file));
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        private FileInfo Check(
            FileInfo file)
        {
            if (!file.Exists)
                throw new Exception(
                    $"Unable to find file '{file.FullName}'");

            return file;
        }
        #endregion

    }
}
