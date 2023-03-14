namespace DStutz.System.Joiners
{
    public interface IJoiner
    {
        public IJoiner Add(params IJoinable?[] joinables);
        public IJoiner Add(params (char align, int width, object? content)[] cells);
        public IJoiner Add(char align, int width, params object?[] contents);

        public string Col { get; }
        public string Row { get; }
        public string RowShort { get; }

        public void WriteCol();
        public void WriteRow();
        public void WriteRowShort();
    }
}
