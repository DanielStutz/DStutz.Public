namespace DStutz.Coder
{
    public abstract class Code
    {

        #region Titles
        /***********************************************************/
        public static string AsymmetricCode = "Asymmetric code";
        public static string AsymmetricKeys = "Asymmetric code (keys)";
        #endregion

        #region Methods region
        /***********************************************************/
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
        #endregion
    }
}
