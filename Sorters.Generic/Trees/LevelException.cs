namespace DStutz.Sorters.Generic.Trees
{
    public class LevelException : Exception
    {
        public LevelException(int level)
            : base($"There is no level {level}") { }
    }
}
