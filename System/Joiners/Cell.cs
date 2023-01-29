namespace DStutz.System.Joiners
{
    public readonly struct Cell
    {
        public object? Content { get; init; }
        public int Width { get; init; }
        public char Align { get; init; }

        public Cell(
            object? content,
            int width)
        {
            Content = content;
            Width = width;
            Align = 'L';
        }

        public Cell(
            object? content,
            int width,
            char align)
        {
            Content = content;
            Width = width;
            Align = align;
        }
    }
}
