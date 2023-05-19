namespace DStutz.Data.Cruders
{
    public interface IValidator
    {
        public string Name { get; }
        public List<ValidationProblem> Problems { get; }
    }

    public interface IValidator<P>
        : IValidator
    {
        public void Validate(P entity);

        //public object? RouteValues(P entity);
    }
}
