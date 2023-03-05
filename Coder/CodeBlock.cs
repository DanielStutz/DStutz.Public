using System.Text;

namespace DStutz.Coder
{
    public class CodeBlock
    {
        #region Properties
        /***********************************************************/
        private string? CursorToken { get; set; }
        private int CursorIndex { get; set; } = 0;
        private string Indent { get; set; } = "";
        private string Append { get; set; } = "";
        private string AppendLast { get; set; } = "";
        private List<string> List { get; } = new List<string>();
        public string[] Lines { get { return List.ToArray(); } }
        #endregion

        #region Constructors
        /***********************************************************/
        public CodeBlock(
            string template)
        {
            List.AddRange(
                template.Split(
                    new string[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None)
                );
        }
        #endregion

        #region Methods handling cursor and appending
        /***********************************************************/
        public CodeBlock SetCursor(
            string token,
            int indent = 0)
        {
            ResetAppend();

            if (indent > 0)
                Indent = "".PadRight(indent);

            CursorToken = "CURSOR_" + token;
            CursorIndex = 0;

            for (int i = 0; i < List.Count; i++, CursorIndex++)
                if (List[CursorIndex].Trim().StartsWith(CursorToken))
                    return this;

            throw new Exception($"Cursor '{CursorToken}' not found");
        }

        public CodeBlock ResetCursor()
        {
            CursorToken = null;
            CursorIndex = 0;

            return this;
        }

        public CodeBlock DeleteCursor()
        {
            if (CursorToken != null)
                Replace(CursorToken, "", CursorIndex);

            return ResetCursor();
        }

        public CodeBlock DeleteCursorLine()
        {
            if (CursorToken != null)
                List.RemoveAt(CursorIndex);

            return ResetCursor();
        }

        public CodeBlock DeleteCursorLines()
        {
            for (int i = List.Count - 1; i >= 0; i--)
                if (List[i].Trim().StartsWith("CURSOR_"))
                    List.RemoveAt(i);

            return ResetCursor();
        }

        public CodeBlock SetAppend(
            string append,
            string appendLast)
        {
            Append = append;
            AppendLast = appendLast;

            return this;
        }

        public CodeBlock ResetAppend()
        {
            return SetAppend("", "");
        }
        #endregion

        #region Methods inserting strings
        /***********************************************************/
        public CodeBlock InsertLine()
        {
            return InsertAppend("", "");
        }

        public CodeBlock Insert(
            params string[] lines)
        {
            if (lines == null ||
                lines.Length == 0)
                return this;

            var last = lines.Length - 1;

            for (int i = 0; i < last; i++)
                InsertAppend(lines[i], Append);

            return InsertAppend(lines[last], AppendLast);
        }

        private CodeBlock InsertAppend(
            string line,
            string append)
        {
            List.Insert(
                CursorIndex++,
                Indent + line + append);

            return this;
        }
        #endregion

        #region Methods inserting another block or a region
        /***********************************************************/
        public CodeBlock Insert(
            CodeBlock code,
            string token,
            int indent = 0)
        {
            return SetCursor(token, indent)
                .Insert(code.Lines);
        }

        public CodeBlock InsertRegionAsymmetricCode(
            int indent = 0)
        {
            return SetCursor("ASYMMETRIC_CODE", indent)
                .Insert(CodeHelper.Region(CodeHelper.AsymmetricCode))
                .Insert("TODO")
                .Insert(CodeHelper.EndRegion());
        }

        public CodeBlock InsertRegion(
            string title,
            string token,
            int indent = 0)
        {
            return SetCursor(token, indent)
                .Insert(CodeHelper.Region(title))
                .Insert(CodeHelper.EndRegion());
        }
        #endregion

        #region Methods inserting items
        /***********************************************************/
        public CodeBlock Insert<T>(
            IEnumerable<T?>? items,
            params Func<T, string[]>[] selectors)
        {
            if (items == null || items.Count() == 0)
                return this;

            List<string> lines = new List<string>();

            foreach (var item in items)
                if (item != null)
                    foreach (var selector in selectors)
                        lines.AddRange(selector(item));

            Insert(lines.ToArray());

            return this;
        }

        public CodeBlock InsertRegion<T>(
            string title,
            IEnumerable<T?>? items,
            params Func<T, string[]>[] selectors)
        {
            if (items == null || items.Count() == 0)
                return this;

            Insert(CodeHelper.Region(title));
            Insert(items, selectors);
            Insert(CodeHelper.EndRegion());

            return this;
        }
        #endregion

        #region Methods replacing
        /***********************************************************/
        public CodeBlock Replace(
            string target,
            string replacement)
        {
            for (int i = 0; i < List.Count; i++)
                List[i] = List[i].Replace(target, replacement);

            return this;
        }

        public CodeBlock Replace(
            string target,
            string replacement,
            int index)
        {
            List[index] = List[index].Replace(target, replacement);

            return this;
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public void Write(
            bool cleanCode,
            bool skipLineNumbers = true)
        {
            Console.WriteLine(GetCode(cleanCode, skipLineNumbers));
        }

        public string GetCode(
            bool cleanCode,
            bool skipLineNumbers = true)
        {
            if (cleanCode)
                CleanCode();

            if (skipLineNumbers)
                return string.Join("\n", List);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < List.Count; i++)
                sb.AppendFormat(
                    "{0,4} {1,1} |{2}\n",
                    i,
                    i == CursorIndex ? "C" : "",
                    List[i]
                );

            return sb.ToString();
        }

        private void CleanCode()
        {
            DeleteCursorLines();

            // Delete empty lines at the beginning
            while (List.Count > 0 &&
                List[0].Trim().Equals(""))
                List.RemoveAt(0);

            // Delete empty lines at the end
            while (List.Count > 0 &&
                List[List.Count - 1].Trim().Equals(""))
                List.RemoveAt(List.Count - 1);

            // Delete multiple empty lines
            for (int i = List.Count - 1; i > 0; i--)
                if (List[i - 1].Trim().Equals("") &&
                    List[i].Trim().Equals(""))
                    List.RemoveAt(i);

            // Delete empty lines after '{'
            for (int i = List.Count - 1; i > 0; i--)
                if (List[i - 1].Trim().Equals("{") &&
                    List[i].Trim().Equals(""))
                    List.RemoveAt(i);

            // Delete empty lines before '}'
            for (int i = List.Count - 1; i > 0; i--)
                if (List[i - 1].Trim().Equals("") &&
                    List[i].Trim().Equals("}"))
                    List.RemoveAt(i - 1);

            // Delete empty lines before '#endregion'
            for (int i = List.Count - 1; i > 0; i--)
                if (List[i - 1].Trim().Equals("") &&
                    List[i].Trim().Equals("#endregion"))
                    List.RemoveAt(i - 1);
        }
        #endregion
    }
}
