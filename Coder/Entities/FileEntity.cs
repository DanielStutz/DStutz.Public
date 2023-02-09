using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities
{
    public class FileEntity : FileBase
    {
        #region Properties
        /***********************************************************/
        public string Version { get; } = "1.1";
        private DataEntity Data { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public FileEntity(
            string template,
            DataEntity data)
            : base(
                  "Entity_" + data.Name + ".cs",
                  template)
        {
            Data = data;
        }
        #endregion

        #region Methods
        /***********************************************************/
        protected void PostProcessing()
        {
            if (Data.Namespace.StartsWith("DStutz.Data"))
                Replace("using DStutz.Data;", "");

            Replace("VERSION", Version);
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
