using DStutz.Coder.Entities.Data;

namespace DStutz.Data.Cruders
{
    public abstract class Validator<P>
        : IValidator<P>
    {
        #region Properties
        /***********************************************************/
        public string Name { get; }
        public List<ValidationProblem> Problems { get; } = new();
        #endregion

        #region Constructors
        /***********************************************************/
        public Validator()
        {
            Name = DataType.Name<P>();
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public void Validate(P entity)
        {
            Check(entity);

            if (Problems.Count > 0)
                throw new ValidationException(Name, Problems);
        }
        #endregion

        #region Methods checking
        /***********************************************************/
        protected abstract void Check(P entity);

        protected void CheckNonNull(
            string property,
            object? value)
        {
            if (value == null)
                Add(property, "is missing");
        }

        protected void CheckMinLength(
            string property,
            string? value,
            int length)
        {
            if (value == null)
                Add(property, "is missing");
            else if (value.Length < length)
                Add(property, value, $"min length is {length}");
        }

        protected void CheckMaxLength(
            string property,
            string? value,
            int length)
        {
            if (value == null)
                Add(property, "is missing");
            else if(value.Length > length)
                Add(property, value, $"max length is {length}");
        }

        protected void CheckLength(
            string property,
            string? value,
            int minLength,
            int maxLength)
        {
            if (value == null)
                Add(property, "is missing");
            else if (value.Length < minLength)
                Add(property, value, $"min length is {minLength}");
            else if (value.Length > maxLength)
                Add(property, value, $"max length is {maxLength}");
        }
        #endregion

        #region Methods adding
        /***********************************************************/
        private void Add(
            string property,
            string message)
        {
            Problems.Add(
                new ValidationProblem()
                {
                    Entity = Name,
                    Property = property,
                    Message = message,
                }
            );
        }

        private void Add(
            string property,
            string value,
            string message)
        {
            Problems.Add(
                new ValidationProblem()
                {
                    Entity = Name,
                    Property = property,
                    Value = value,
                    Message = message,
                }
            );
        }
        #endregion
    }
}
