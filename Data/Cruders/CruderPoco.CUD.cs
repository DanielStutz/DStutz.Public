namespace DStutz.Data.CRUD
{
    public abstract partial class CruderPoco<E, P, I>
    {
        #region Methods creating
        /***********************************************************/
        public virtual P Create(
            bool saveChanges,
            P poco)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods updating
        /***********************************************************/
        public virtual ValueTask<P> DeleteCreate(
            bool saveChanges,
            P poco,
            params object[] primaryKeys)
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<P> Update(
            bool saveChanges,
            P poco,
            params object[] primaryKeys)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
