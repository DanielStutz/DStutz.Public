namespace DStutz.View.Labels
{
    public class Label
    {
        #region Properties
        /***********************************************************/
        public List<string> Lines { get; } = new List<string>();
        #endregion

        #region Constructors
        /***********************************************************/
        public Label()
        { }

        public Label(params string[] lines)
        {
            AppendLines(lines);
        }
        #endregion

        #region Methods handling lines
        /***********************************************************/
        public void AppendLine(string line)
        {
            if (line == null || line.Length == 0)
                throw new Exception("Line is empty");

            Lines.Add(line);
        }

        public void AppendLines(ICollection<string> lines)
        {
            if (lines == null || lines.Count == 0)
                throw new Exception("No lines provided");

            foreach (string line in lines)
                AppendLine(line);
        }

        public void PrependLine(string line)
        {
            if (line == null || line.Length == 0)
                throw new Exception("Line is empty");

            Lines.Insert(0, line);
        }

        public void PrependLines(ICollection<string> lines)
        {
            if (lines == null || lines.Count == 0)
                throw new Exception("No lines provided");

            foreach (string line in lines)
                PrependLine(line);
        }

        public string GetLine(int index)
        {
            return Lines[index];
        }

        public List<string> GetLines()
        {
            return Lines;
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string ToString()
        {
            return string.Join("\n", Lines);
        }
        #endregion
    }
}
