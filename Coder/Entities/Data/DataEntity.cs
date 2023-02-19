namespace DStutz.Coder.Entities.Data
{
    // The Comment will be added to the code file,
    // the Warning is to be read by the programer.
    public abstract class DataEntity
    {
        #region Properties of all entities
        /***********************************************************/
        public string Version { get; set; }
        public bool AsymmetricCode { get; }
        public string Namespace { get; }
        public string Name { get; }
        public string? Comment { get; }
        public string? Warning { get; }
        public DataTypeEntity Type { get; }
        public List<DataPropertyColumn> Properties { get; } = new();
        #endregion

        #region Constructors
        /***********************************************************/
        protected DataEntity(
            JsonEntity entity,
            string suffixEfco,
            string suffixPoco)
        {
            Version = entity.Version;
            AsymmetricCode = entity.AsymmetricCode;
            Namespace = entity.Namespace;
            Name = entity.Name;
            Comment = entity.Comment;
            Warning = entity.Warning;
            Type = new DataTypeEntity(entity, suffixEfco, suffixPoco);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public string GetNamespaceEfco()
        {
            return Namespace.Replace("#", "Efcos");
        }

        public string GetNamespacePoco()
        {
            return Namespace.Replace("#", "Pocos");
        }
        #endregion
    }
}
