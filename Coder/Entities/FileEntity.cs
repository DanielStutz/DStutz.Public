using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities
{
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
                  data.Warning,
                  "1.1")
        {
            Data = data;

            if (!data.Version.Equals(Version))
                throw new Exception(
                    $"Version {data.Version} of json file does not match " +
                    $"version {Version} of class {typeof(FileEntity).Name}");
        }
        #endregion

        #region Methods
        /***********************************************************/
        protected override void PostProcessing()
        {
            base.PostProcessing();

            if (Data.Namespace.StartsWith("DStutz.Data"))
                Replace("using DStutz.Data;", "");

            Replace("NAMESPACE_EFCO", Data.GetNamespaceEfco());
            Replace("NAMESPACE_POCO", Data.GetNamespacePoco());
            Replace("TYPE_INTERFACE", Data.Type.I);
            Replace("TYPE_POCO", Data.Type.P);
            Replace("TYPE_EFCO", Data.Type.E);
            Replace("TYPE_MAPPER", Data.Type.M);
        }
        #endregion
    }
}
