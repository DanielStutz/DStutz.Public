namespace DStutz.System.Joiners
{
    public interface IJoinable
    {
        public IJoiner Joiner { get; }
    }

    public interface IJoinableNew
    {
        public IJoiner Joiner();
    }
}
