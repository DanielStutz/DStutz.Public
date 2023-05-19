namespace DStutz.Data.Cruders
{
    public partial class CruderPoco<E, P, I>
    {
        #region Methods validating
        /***********************************************************/
        public IValidator<P> Validator { get; set; }

        private void Validate(
            P poco)
        {
            if (Validator == null)
                throw new Exception("No validator set");

            Validator.Validate(poco);
        }
        #endregion

        #region Methods creating
        /***********************************************************/
        public async ValueTask<P> Create(
            P poco,
            bool validate,
            bool saveChanges)
        {
            if (validate)
                Validate(poco);

            return (await Create(poco.Map<E>(), saveChanges)).Map();
        }
        #endregion

        #region Methods updating
        /***********************************************************/
        public async virtual ValueTask<P> DeleteCreate(
            long primaryKey,
            P poco,
            bool validate,
            bool saveChanges)
        {
            if (validate)
                Validate(poco);

            await Delete(primaryKey, true);

            return await Create(poco, false, saveChanges);
        }

        public async virtual ValueTask<P> DeleteCreate(
            object[] primaryKeys,
            P poco,
            bool validate,
            bool saveChanges)
        {
            if (validate)
                Validate(poco);

            await Delete(primaryKeys, true);

            return await Create(poco, false, saveChanges);
        }

        public ValueTask<P> Update(
            P poco,
            bool validate,
            bool saveChanges)
        {
            if (validate)
                Validate(poco);

            return Update(poco, saveChanges);
        }

        protected virtual ValueTask<P> Update(
            P poco,
            bool saveChanges)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
