namespace DStutz.Coder.Entities.Data
{
    public abstract class DataEntity
    {
        #region Properties of all entities
        /***********************************************************/
        public CodeInfoEntity Code { get; set; }
        public string Namespace { get; }
        public string Name { get; }
        public DataTypeEntity Type { get; }
        public List<DataPropertyColumn> Properties { get; } = new();
        #endregion

        #region Properties of non-owned (basic, relations) entities
        /***********************************************************/
        public bool Abstract { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected DataEntity(
            JsonEntity entity,
            string suffixEfco,
            string suffixPoco)
        {
            Abstract = entity.Abstract;
            Code = entity.Code;
            Namespace = entity.Namespace;
            Name = entity.Name;
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
