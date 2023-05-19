namespace DStutz.Data.Cruders
{
    public class ValidationException
        : Exception
    {
        #region Properties
        /***********************************************************/
        public override string Message { get; }
        public IEnumerable<ValidationProblem> Problems { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public ValidationException(
            string entity,
            IEnumerable<ValidationProblem> problems)
        {
            Message = $"Validation of {entity} failed";
            Problems = problems;
        }
        #endregion
    }
}
