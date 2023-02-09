namespace DStutz.Coder
{
    public abstract class Code
    {
        public static string[] Region(
            string title)
        {
            return new string[] {
                "",
                "#region " + title,
                "/***********************************************************/"
            };
        }

        public static string[] EndRegion()
        {
            return new string[] {
                "#endregion",
                ""
            };
        }
    }
}
