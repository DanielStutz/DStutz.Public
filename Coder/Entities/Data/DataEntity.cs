namespace DStutz.Coder.Entities.Data
{
    public abstract class DataEntity
    {
        #region Properties of all entities
        /***********************************************************/
        public string Namespace { get; }
        public string Name { get; }
        public DataType Type { get; }
        public List<DataPropertyColumn> Properties { get; set; } = new List<DataPropertyColumn>();
        #endregion

        #region Constructors
        /***********************************************************/
        protected DataEntity(
            JsonEntity entity,
            string suffixEfco,
            string suffixPoco)
        {
            Namespace = entity.Namespace;
            Name = entity.Name;
            Type = new DataType(entity, suffixEfco, suffixPoco);
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
