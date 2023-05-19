namespace DStutz.Data.Cruders
{
    public interface ICruder
    {
        public const int INCLUDE_NONE = int.MinValue;
        public const int INCLUDE_ALL = int.MaxValue;

        // See DStutz.System.Unique for assignments
        public const int INCLUDE_IMAGES = 19;
        public const int INCLUDE_NAMES = 24;

        #region Properties
        /***********************************************************/
        public string Name { get; }
        #endregion

        #region Methods counting and existing
        /***********************************************************/
        public int Count();
        public bool Exists(long primaryKey);
        public bool Exists(object[] primaryKeys);
        #endregion

        #region Methods deleting
        /***********************************************************/
        public ValueTask<int> Delete(long primaryKey, bool saveChanges = true);
        public ValueTask<int> Delete(object[] primaryKeys, bool saveChanges = true);
        public ValueTask<int> DeleteAll(bool saveChanges = true);
        #endregion
    }

    public partial interface ICruder<P>
        : ICruder
    {
        #region Methods validating
        /***********************************************************/
        public IValidator<P> Validator { get; set; }
        #endregion

        #region Methods creating
        /***********************************************************/
        public ValueTask<P> Create(P poco, bool validate = true, bool saveChanges = true);
        #endregion

        #region Methods reading one entity
        /***********************************************************/
        public ValueTask<P?> ReadOrDefault(long primaryKey, int includeType = INCLUDE_ALL);
        public ValueTask<P> ReadOrThrow(long primaryKey, int includeType = INCLUDE_ALL);
        public ValueTask<P?> ReadOrDefault(object[] primaryKeys, int includeType = INCLUDE_ALL);
        public ValueTask<P> ReadOrThrow(object[] primaryKeys, int includeType = INCLUDE_ALL);
        #endregion

        #region Methods reading first entity
        /***********************************************************/
        // Any methods using predicates have to be implemented
        // in derived classes like CruderArticle or CruderVideo
        // since this interface ICruder<P> knows P but doesn't
        // know the EF Core class E or their common interface I
        #endregion

        #region Methods reading all entities
        /***********************************************************/
        public Task<List<P>> ReadAll(int includeType = INCLUDE_ALL);
        public Task<List<T>> ReadAll<T>(Func<P, T> selector, int includeType = INCLUDE_ALL);
        #endregion

        #region Methods reading many entities
        /***********************************************************/
        // Any methods using predicates ... (see remark above)
        #endregion

        #region Methods updating
        /***********************************************************/
        public ValueTask<P> DeleteCreate(long primaryKey, P poco, bool validate = true, bool saveChanges = true);
        public ValueTask<P> DeleteCreate(object[] primaryKeys, P poco, bool validate = true, bool saveChanges = true);
        public ValueTask<P> Update(P poco, bool validate = true, bool saveChanges = true);
        #endregion
    }
}
