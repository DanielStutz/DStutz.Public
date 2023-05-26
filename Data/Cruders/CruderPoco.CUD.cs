namespace DStutz.Data.Cruders
{
    public partial class CruderPoco<E, P, I>
    {
        #region Methods validating
        /***********************************************************/
        public bool OmitValidation { get; set; } = false;
        public IValidator<P> Validator { get; set; }

        private void Validate(
            P poco)
        {
            if (OmitValidation)
                return;

            if (Validator == null)
                throw new Exception($"No validator set in {GetType().Name}");

            Validator.Validate(poco);
        }
        #endregion

        #region Methods creating
        /***********************************************************/
        public async virtual ValueTask<P> Create(
            P poco,
            bool validate,
            bool saveChanges)
        {
            if (validate)
                Validate(poco);

            SetPK(poco);

            return (await Create(poco.Map<E>(), saveChanges)).Map();
        }

        public virtual void SetPK(P poco)
        {
            // Overwrite if P needs specific primary key(s)
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
